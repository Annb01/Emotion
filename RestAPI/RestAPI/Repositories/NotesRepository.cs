using MongoDB.Driver;
using RestAPI.Models;

namespace RestAPI.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private readonly IMongoCollection<UserNote> _notes;

        public NotesRepository(IMongoDatabase database)
        {
            _notes = database.GetCollection<UserNote>("Notes");
        }

        public async Task CreateAsync(UserNote note) => await _notes.InsertOneAsync(note);

        public async Task<List<UserNote>> GetUserNotesAsync(string userId) =>
            await _notes.Find(n => n.UserId == userId).ToListAsync();

        public async Task<UserNote> GetByIdAsync(string id) =>
            await _notes.Find(n => n.Id == id).FirstOrDefaultAsync();
        public async Task UpdateAsync(UserNote note)
        {
            await _notes.ReplaceOneAsync(n => n.Id == note.Id, note);
        }
    }
}
