using System.ComponentModel.DataAnnotations;

namespace Hexaplanning.Models
{
    public class ThemeCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        public Theme ToTheme(Guid userId)
        {
            return new Theme
            {
                Name = Name,
                Color = Color,
                UserId = userId
            };
        }
    }
}
