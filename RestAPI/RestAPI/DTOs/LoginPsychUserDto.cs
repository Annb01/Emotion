namespace RestAPI.DTOs
{
    public class LoginPsychUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Roles {  get; set; }
    }
}
