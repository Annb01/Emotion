using System.ComponentModel.DataAnnotations;

namespace RestAPI.DTOs
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string Surname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Niepoprawny format e-mail")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Hasło musi mieć min 6 znaków")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Kod od psychiatry jest niezbędny do rejestracji")]
        public string PsychiatryistCode { get; set; }
    }
}
