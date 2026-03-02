using RestAPI.Models;

namespace RestAPI.Repositories
{
    public interface IPsychRepository
    {
        Task<Psychiatrist?> GetByEmailAsync(string email);
        Task AddAsync(Psychiatrist psychiatrist);
    }
}
