using System.ComponentModel.DataAnnotations;

namespace Hexaplanning.Models
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
