using MongoDB.Driver;
using RestAPI.Models;

namespace RestAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("Users");
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task AddAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            var result = await _users.ReplaceOneAsync(u => u.Id == user.Id, user);

            if (!result.IsAcknowledged || result.MatchedCount == 0)
            {
                throw new Exception("Nie udało się zaktualizować użytkownika w bazie");
            }
        }
    }
}