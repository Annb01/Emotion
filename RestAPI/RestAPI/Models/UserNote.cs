namespace RestAPI.Models
{
    public class UserNote
    {
        public string Id { get; set; }

        public string UserId { get; set; } 
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsAnalyzed { get; set; } = false;
    }
}
