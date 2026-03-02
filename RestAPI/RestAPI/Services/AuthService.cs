using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RestAPI.DTOs;
using RestAPI.DTOs.Auth;
using RestAPI.Generator;
using RestAPI.Models;
using RestAPI.Repositories;
using RestAPI.TokenGenerator;
using System.Data;

namespace RestAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher<User> _hasher;
        private readonly IJwtTokenGenerator _jwt;
        private readonly UserManager<User> _userManager;
        private readonly UserManager<Psychiatrist> _psychiatristManager;
        private readonly IMongoCollection<NewPsychCode> _newPsychCodeCollection;

        public AuthService(IPasswordHasher<User> hasher, IJwtTokenGenerator jwt, UserManager<User> userManager, UserManager<Psychiatrist> psychiatristManager, IMongoCollection<NewPsychCode> newPsychCodeCollection)
        {
            _hasher = hasher;
            _jwt = jwt;
            _userManager = userManager;
            _psychiatristManager = psychiatristManager;
            _newPsychCodeCollection = newPsychCodeCollection;
        }

        public async Task<AuthResult> RegisterUserAsync(RegisterUserDto model)
        {
            var psychiatrist = await _psychiatristManager.Users
                .FirstOrDefaultAsync(p => p.PatientAddCode == model.PsychiatryistCode);

            if (psychiatrist == null)
            {
                return new AuthResult { Success = false, Message = "Kod jest nieprawidłowy." };
            }

            var user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.Name,
                LastName = model.Surname,
                AssignedPsychiatristId = psychiatrist.Id, 
                Roles = new List<string> { "User" }
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new AuthResult { Success = false, Errors = result.Errors.Select(e => e.Description) };
            }

            return new AuthResult { Success = true };
        }

        public async Task<AuthResult> RegisterPsychiatristAsync(RegisterPsychDto model)
        {
            var registrationCode = await _newPsychCodeCollection
            .Find(c => c.Code == model.PsychiatryistRegisterCode
                       && !c.IsUsed
                       && (c.ExpiresAt == null || c.ExpiresAt > DateTime.UtcNow))
            .FirstOrDefaultAsync();
            var allCodes = await _newPsychCodeCollection.Find(_ => true).ToListAsync();
            Console.WriteLine($"W bazie jest {allCodes.Count} kodów.");

            if (registrationCode == null)
            {
                return new AuthResult { Success = false, Message = "Kod rejestracji jest nieprawidłowy lub został już wykorzystany" };
            }

            var psychiatrist = new Psychiatrist
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.Name,
                LastName = model.Surname,
                PatientAddCode = PsychCodeGenerator.GeneratePsychiatristCode(),
                Roles = new List<string> { "Psychiatrist" }
            };

            var result = await _psychiatristManager.CreateAsync(psychiatrist, model.Password);

            if (!result.Succeeded)
            {
                return new AuthResult { Success = false, Errors = result.Errors.Select(e => e.Description) };
            }

            await _newPsychCodeCollection.UpdateOneAsync(
                c => c.Code == model.PsychiatryistRegisterCode,
                Builders<NewPsychCode>.Update.Set(c => c.IsUsed, true)
            );

            return new AuthResult { Success = true, Message = "Rejestracja psychiatry pomyślna" };
        }


        public async Task<AuthResult> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null)
            {
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
                if (!isPasswordValid) return AuthResult.Fail("Błędny email lub hasło");

                var roles = user.Roles ?? new List<string> { "User" };

                var token = _jwt.GenerateToken(user.Id, user.Email, roles);
                return AuthResult.Ok(token, new LoginPsychUserDto { Id = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Roles = user.Roles?.FirstOrDefault() ?? "User" });
            }

            var psych = await _psychiatristManager.FindByEmailAsync(dto.Email);
            if (psych != null)
            {
                var isPasswordValid = await _psychiatristManager.CheckPasswordAsync(psych, dto.Password);
                if (!isPasswordValid) return AuthResult.Fail("Błędny email lub hasło");

                var roles = psych.Roles ?? new List<string> { "Psychiatrist" };

                var token = _jwt.GenerateToken(psych.Id, psych.Email, roles);
                return AuthResult.Ok(token, new LoginPsychUserDto { Id = psych.Id, Email = psych.Email, FirstName = psych.FirstName, LastName = psych.LastName, Roles = psych.Roles?.FirstOrDefault() ?? "Psychiatrist" });
            }

            return AuthResult.Fail("Nie znaleziono użytkownika");
        }

    }
}