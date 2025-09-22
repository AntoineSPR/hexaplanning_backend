using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }

    public class ResetPasswordDTO
    {
        [Required]
        public string Token { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 2)]
        public string NewPassword { get; set; } = null!;
    }
}

