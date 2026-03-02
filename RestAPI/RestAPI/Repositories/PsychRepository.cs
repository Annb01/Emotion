using MongoDB.Driver;
using RestAPI.Models;

namespace RestAPI.Repositories
{
    public class PsychRepository
    {
        private readonly IMongoCollection<Psychiatrist> _psychiatrist;

        public PsychRepository(IMongoDatabase database)
        {
            _psychiatrist = database.GetCollection<Psychiatrist>("Psychiatrist");
        }

        public async Task<Psychiatrist?> GetByEmailAsync(string email)
        {
            return await _psychiatrist.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Psychiatrist psychiatrist)
        {
            await _psychiatrist.InsertOneAsync(psychiatrist);
        }
    }
}
