using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class QuestUpdateDTO
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int EstimatedTime { get; set; }

        public Guid PriorityId { get; set; }
        public Guid StatusId { get; set; }
        public int? Advancement { get; set; }

        public HexAssignmentDTO? HexAssignment { get; set; }

        public void UpdateQuest(Quest existingQuest)
        {
            existingQuest.Title = Title;
            existingQuest.Description = Description;
            existingQuest.EstimatedTime = EstimatedTime;
            existingQuest.PriorityId = PriorityId;
            existingQuest.StatusId = StatusId;
            existingQuest.Advancement = Advancement;
            existingQuest.HexAssignment = HexAssignment != null ? HexAssignment.ToHexAssignment() : null;
        }

    }
}
