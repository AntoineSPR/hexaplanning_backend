using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class UserApp : IdentityUser<Guid>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public List<Quest> QuestList { get; set; }

        // Methodes de conversion entre differents model dto

        public UserResponseDTO ToUserResponseDTO()
        {
            return new UserResponseDTO
            {
                Email = Email ?? "",
                FirstName = FirstName,
                LastName = LastName,
                UserId = Id
            };
        }

        public UserResponseDTO ToUserResponseDTO(List<string> roles)
        {
            return new UserResponseDTO
            {
                Email = Email ?? "",
                FirstName = FirstName,
                LastName = LastName,
                Roles = roles,
                UserId = Id
            };
        }
    }
}
