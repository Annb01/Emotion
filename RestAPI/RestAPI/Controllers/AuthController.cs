using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RestAPI.DTOs;
using RestAPI.DTOs.Auth;
using RestAPI.Models;
using RestAPI.Services;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet("check-db")]
        public async Task<IActionResult> CheckDbConnection()
        {
            try
            {
                var database = _authService is AuthService authService
                    ? typeof(AuthService).GetField("_newPsychCodeCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(authService) as IMongoCollection<NewPsychCode>
                    : null;

                return Ok(new
                {
                    Status = "Sukces",
                    Message = "Połączenie z MongoDB działa",
                    Timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Błąd",
                    Message = "Nie udało się połączyć z bazą danych",
                    Details = ex.Message
                });
            }
        }
        [HttpPost("register-psych")]
        public async Task<IActionResult> RegisterPsych([FromBody] RegisterPsychDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(AuthResult.Fail("Niepoprawne dane wejściowe"));

            var result = await _authService.RegisterPsychiatristAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(new { Message = "Użytkownik zarejestrowany pomyślnie" });
        }
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(AuthResult.Fail("Niepoprawne dane wejściowe"));

            var result = await _authService.RegisterUserAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(new { Message = "Użytkownik zarejestrowany pomyślnie" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
                return Unauthorized(result);


            return Ok(result);
        }
    }
}
