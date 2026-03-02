using Microsoft.AspNetCore.Identity;
using RestAPI.DTOs;
using RestAPI.DTOs.Auth;
using RestAPI.DTOs.Notes;
using RestAPI.Models;
using RestAPI.Repositories;
using System.Net.Http;

namespace RestAPI.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _notesRepository;
        private readonly IEmotionRepository _emotionRepository;
        private readonly HttpClient _httpClient;

        public NotesService(INotesRepository notesRepository, IEmotionRepository emotionRepository, HttpClient httpClient)
        {
            _notesRepository = notesRepository;
            _emotionRepository = emotionRepository;
            _httpClient = httpClient;
        }

        public async Task<UserNote> AddNoteAsync(string userId, string content)
        {
            var note = new UserNote
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            await _notesRepository.CreateAsync(note);
            return note;
        }
        public async Task<GetAllNotesResult> GetAllUserNotesAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return GetAllNotesResult.FailWithMessage("Nieprawidłowe ID użytkownika");
            }

            try
            {
                var notes = await _notesRepository.GetUserNotesAsync(userId);

                if (notes == null || !notes.Any())
                {
                    return GetAllNotesResult.Empty("Twoja lista notatek jest pusta");
                }

                return GetAllNotesResult.Ok(notes);
            }
            catch (Exception ex)
            {
                return GetAllNotesResult.FailWithMessage($"Błąd bazy danych: {ex.Message}");
            }
        }
        public async Task<AnalyzeNoteResult> AnalyzeNotesAsync(string noteId, string userId)
        {
            var note = await _notesRepository.GetByIdAsync(noteId);
            if (note == null || note.UserId != userId)
                return AnalyzeNoteResult.Fail("Nie znaleziono notatki lub brak uprawnień");

            if (note.IsAnalyzed)
                return AnalyzeNoteResult.Fail("Notatka została już poddana analizie");

            try
            {
                var aiRequest = new { text = note.Content };

                var response = await _httpClient.PostAsJsonAsync("http://127.0.0.1:8000/analyze", aiRequest);

                if (!response.IsSuccessStatusCode)
                {
                    var errorDetail = await response.Content.ReadAsStringAsync();
                    return AnalyzeNoteResult.Fail($"Serwer AI zwrócił błąd ({response.StatusCode}): {errorDetail}");
                }

                var aiResult = await response.Content.ReadFromJsonAsync<PythonAiResponse>();

                var analysis = new EmotionAnalysis
                {
                    Id = Guid.NewGuid().ToString(),
                    NoteId = noteId,
                    Label = aiResult.Emotion,
                    Confidence = aiResult.Confidence,
                    AnalyzedAt = DateTime.UtcNow
                };

                await _emotionRepository.CreateAsync(analysis);

                note.IsAnalyzed = true;
                await _notesRepository.UpdateAsync(note);

                return AnalyzeNoteResult.Ok(analysis);
            }
            catch (HttpRequestException ex)
            {
                return AnalyzeNoteResult.Fail($"Problem z połączeniem do serwera AI {ex.Message}");
            }
            catch (Exception ex)
            {
                return AnalyzeNoteResult.Fail($"Wystąpił błąd {ex.Message}");
            }
        }
    }
}
