using System.ComponentModel.DataAnnotations;

namespace Hexaplanning.Models
{
    public class RefreshTokenRequestDTO
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
