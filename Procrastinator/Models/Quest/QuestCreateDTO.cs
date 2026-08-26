using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Procrastinator.Models
{
    public class QuestCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string? Description { get; set; }

        public int EstimatedTime { get; set; }

        public Guid StatusId { get; set; }
        public int? Advancement { get; set; }

        public Guid? ThemeId { get; set; }
        public bool IsPrimaryTheme { get; set; }

        public Quest ToQuest(Guid userId)
        {
            return new Quest
            {
                Title = Title,
                Description = Description,
                UserId = userId,
                EstimatedTime = EstimatedTime,
                StatusId = StatusId,
                Advancement = Advancement,
                ThemeId = ThemeId,
                IsPrimaryTheme = IsPrimaryTheme
            };
        }
    }
}
