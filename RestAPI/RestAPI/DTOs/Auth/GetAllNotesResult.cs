using RestAPI.Models;

namespace RestAPI.DTOs.Notes
{
    public class GetAllNotesResult
    {
        public bool Success { get; init; }
        public IEnumerable<UserNote> Notes { get; init; }
        public IEnumerable<string>? Errors { get; init; }
        public string? Message { get; set; }

        public static GetAllNotesResult Ok(IEnumerable<UserNote> notes)
            => new GetAllNotesResult { Success = true, Notes = notes };

        public static GetAllNotesResult Empty(string message = "Nie znaleziono żadnych notatek")
        => new GetAllNotesResult
        {
            Success = true,
            Notes = new List<UserNote>(),
            Message = message
        };
        public static GetAllNotesResult Fail(params string[] errors)
            => new GetAllNotesResult { Success = false, Errors = errors, Notes = new List<UserNote>() };

        public static GetAllNotesResult FailWithMessage(string message)
            => new GetAllNotesResult { Success = false, Message = message, Notes = new List<UserNote>() };
    }
}