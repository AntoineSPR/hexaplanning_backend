using Microsoft.AspNetCore.Identity;
using Procrastinator.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class UserApp : IdentityUser<Guid>, IArchivable, ICreatable, IUpdatable
    {
        [Required]
        public string Name { get; set; }
        public List<Quest> QuestList { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsArchived { get; set; }

        // Methodes de conversion entre differents model dto

        public UserResponseDTO ToUserResponseDTO()
        {
            return new UserResponseDTO
            {
                Email = Email ?? "",
                Name = Name,
                UserId = Id
            };
        }

        public UserResponseDTO ToUserResponseDTO(List<string> roles)
        {
            return new UserResponseDTO
            {
                Email = Email ?? "",
                Name = Name,
                Roles = roles,
                UserId = Id
            };
        }
    }
}
