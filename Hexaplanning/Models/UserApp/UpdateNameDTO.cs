using System.ComponentModel.DataAnnotations;

namespace Hexaplanning.Models
{
    public class UpdateNameDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
