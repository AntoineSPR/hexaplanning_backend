using System.ComponentModel.DataAnnotations;

namespace Hexaplanning.Models
{
    public class ThemeUpdateDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        public void UpdateTheme(Theme existing)
        {
            existing.Name = Name;
            existing.Color = Color;
        }
    }
}
