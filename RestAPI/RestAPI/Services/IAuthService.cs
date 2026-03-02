using RestAPI.DTOs;
using RestAPI.Models;
using RestAPI.Repositories;
using RestAPI.DTOs.Auth;

namespace RestAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterUserAsync(RegisterUserDto model);
        Task<AuthResult> RegisterPsychiatristAsync(RegisterPsychDto model);
        Task<AuthResult> LoginAsync(LoginDto dto);
    }
}
