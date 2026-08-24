using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class QuestGroupCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        public List<Guid> QuestIds { get; set; } = new List<Guid>();

        public QuestGroup ToQuestGroup(Guid userId)
        {
            return new QuestGroup
            {
                Name = Name,
                Color = Color,
                UserId = userId
            };
        }
    }
}
