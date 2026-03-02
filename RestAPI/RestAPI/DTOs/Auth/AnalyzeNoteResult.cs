using RestAPI.Models;

namespace RestAPI.DTOs.Notes
{
    public class AnalyzeNoteResult
    {
        public bool Success { get; init; }
        public EmotionAnalysis? Analysis { get; init; }
        public IEnumerable<string>? Errors { get; init; }
        public string? Message { get; set; }

        public static AnalyzeNoteResult Ok(EmotionAnalysis analysis)
            => new AnalyzeNoteResult { Success = true, Analysis = analysis };

        public static AnalyzeNoteResult Fail(params string[] errors)
            => new AnalyzeNoteResult { Success = false, Errors = errors };

        public static AnalyzeNoteResult FailWithMessage(string message)
            => new AnalyzeNoteResult { Success = false, Message = message };
    }
}