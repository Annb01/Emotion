using MongoDB.Driver;
using RestAPI.Models;

namespace RestAPI.Repositories
{
    public class EmotionRepository : IEmotionRepository
    {
        private readonly IMongoCollection<EmotionAnalysis> _emotions;

        public EmotionRepository(IMongoDatabase database)
        {
            _emotions = database.GetCollection<EmotionAnalysis>("EmotionAnalyses");
        }

        public async Task CreateAsync(EmotionAnalysis analysis) =>
            await _emotions.InsertOneAsync(analysis);

        public async Task<EmotionAnalysis> GetByNoteIdAsync(string noteId) =>
            await _emotions.Find(e => e.NoteId == noteId).FirstOrDefaultAsync();
    }
}
