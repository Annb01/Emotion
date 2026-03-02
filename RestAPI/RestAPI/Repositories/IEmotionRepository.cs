using RestAPI.Models;

namespace RestAPI.Repositories
{
    public interface IEmotionRepository
    {
        Task CreateAsync(EmotionAnalysis analysis);
        Task<EmotionAnalysis> GetByNoteIdAsync(string noteId);
    }
}
