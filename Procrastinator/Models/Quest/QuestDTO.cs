using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class QuestDTO
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public int EstimatedTime { get; set; }

        public Guid PriorityId { get; set; }
        public Guid StatusId { get; set; }

        public Guid? HexAssignmentId => HexAssignment?.Id;
        public HexAssignmentDTO? HexAssignment { get; set; }

        public Quest ToQuest()
        {
            return new Quest
            {
                Title = Title,
                Description = Description,
                UserId = UserId,
                EstimatedTime = EstimatedTime,
                PriorityId = PriorityId,
                StatusId = StatusId,
                HexAssignment = HexAssignment != null ? HexAssignment.ToHexAssignment() : null
            };
        }

        public static QuestDTO ToQuestDTO(Quest quest)
        {
            return new QuestDTO
            {
                Id = quest.Id,
                Title = quest.Title,
                Description = quest.Description,
                UserId = quest.UserId,
                EstimatedTime = quest.EstimatedTime,
                //Priority = quest.Priority,
                //IsDone = quest.IsDone,
                //IsAssigned = quest.IsAssigned,
                HexAssignment = quest.HexAssignment != null ? HexAssignmentDTO.ToHexAssignmentDTO(quest.HexAssignment) : null
            };

        }
        
    }
}
