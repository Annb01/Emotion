using Microsoft.Azure.Cosmos;
using RestAPI.Models;

namespace RestAPI.Repositories
{
    public interface INotesRepository
    {
        Task CreateAsync(UserNote note);
        Task<List<UserNote>> GetUserNotesAsync(string userId);
        Task<UserNote> GetByIdAsync(string id);
        Task UpdateAsync(UserNote note);
    }
}
