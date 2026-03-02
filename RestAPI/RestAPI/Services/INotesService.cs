using Microsoft.Azure.Cosmos;
using RestAPI.DTOs.Notes;
using RestAPI.Models;

namespace RestAPI.Services
{
    public interface INotesService
    {
        Task<UserNote> AddNoteAsync(string userId, string content);
        Task<GetAllNotesResult> GetAllUserNotesAsync(string userId);
        Task<AnalyzeNoteResult> AnalyzeNotesAsync(string noteId, string userId);
    }
}
