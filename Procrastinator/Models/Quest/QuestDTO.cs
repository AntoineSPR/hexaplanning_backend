using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class QuestDTO
    {
        public Guid? Id { get; set; }

        // Read-only: set by BaseModel at entity creation, never accepted from client input (not on
        // QuestCreateDTO/QuestUpdateDTO) - exposed so the frontend has a real timestamp to sort
        // "date added" by, instead of relying on array/insertion order.
        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public int EstimatedTime { get; set; }

        public Guid? ThemeId { get; set; }
        public bool IsPrimaryTheme { get; set; }
        public Guid StatusId { get; set; }
        public int? Advancement { get; set; }

        public HexAssignmentDTO? HexAssignment { get; set; }

        public Guid? QuestGroupId { get; set; }

        public Quest ToQuest()
        {
            return new Quest
            {
                Title = Title,
                Description = Description,
                UserId = UserId,
                EstimatedTime = EstimatedTime,
                ThemeId = ThemeId,
                IsPrimaryTheme = IsPrimaryTheme,
                StatusId = StatusId,
                HexAssignment = HexAssignment != null ? HexAssignment.ToHexAssignment() : null,
                Advancement = Advancement,
                QuestGroupId = QuestGroupId
            };
        }

        public static QuestDTO ToQuestDTO(Quest quest)
        {
            return new QuestDTO
            {
                Id = quest.Id,
                CreatedAt = quest.CreatedAt,
                Title = quest.Title,
                Description = quest.Description,
                UserId = quest.UserId,
                EstimatedTime = quest.EstimatedTime,
                StatusId = quest.StatusId,
                ThemeId = quest.ThemeId,
                IsPrimaryTheme = quest.IsPrimaryTheme,
                Advancement = quest.Advancement,
                HexAssignment = quest.HexAssignment != null ? HexAssignmentDTO.ToHexAssignmentDTO(quest.HexAssignment) : null,
                QuestGroupId = quest.QuestGroupId
            };
        }
        
    }
}
