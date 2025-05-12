using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class UserCreateDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public UserApp ToUserApp()
        {
            return new UserApp
            {
                Email = Email,
                UserName = Email,
                FirstName = FirstName,
                LastName = LastName
            };
        }

        public UserApp ToSimpleUser(UserApp user)
        {
            user.FirstName = FirstName;
            user.LastName = LastName;
            return user;
        }
    }
}
