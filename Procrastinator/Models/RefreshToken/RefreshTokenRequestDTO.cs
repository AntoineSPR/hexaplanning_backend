using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class RefreshTokenRequestDTO
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
