namespace RestAPI.Models
{
    public class EmotionAnalysis
    {
        public string Id { get; set; }
        public string NoteId { get; set; } 
        public string Label { get; set; } 
        public double Confidence { get; set; }
        public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;
    }
}
