using System.Security.Claims;

namespace RestAPI.TokenGenerator
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string email, IEnumerable<string> roles);
    }
}