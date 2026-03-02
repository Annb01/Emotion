using RestAPI.DTOs;

namespace RestAPI.DTOs.Auth
{
    public class AuthResult
    {
        public bool Success { get; init; }
        public string Token { get; init; }
        public LoginPsychUserDto User { get; init; }
        public IEnumerable<string> Errors { get; init; }
        public string? Message { get; set; }

        public static AuthResult Ok(string token, LoginPsychUserDto psychUser)
        => new AuthResult { Success = true, Token = token, User = psychUser };

        public static AuthResult Fail(params string[] errors)
            => new AuthResult { Success = false, Errors = errors };

        public static AuthResult FailWithMessage(string message)
            => new AuthResult { Success = false, Message = message };
    }
}