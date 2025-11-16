using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models.UserApp
{
    public class RefreshTokenRequestDTO
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}