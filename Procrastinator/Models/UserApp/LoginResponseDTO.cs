namespace Procrastinator.Models
{
    public class LoginResponseDTO

    {
        public string Token { get; set; }
        public UserResponseDTO User { get; set; }
    }
}
