using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class Quest
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public UserApp User { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public int ExperienceGain { get; set; }

        public int Apprehension { get; set; }

        public int EstimatedTime { get; set; }

        public int Difficulty { get; set; }

    }
}
