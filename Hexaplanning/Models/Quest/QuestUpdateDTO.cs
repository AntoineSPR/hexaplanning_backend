using System.ComponentModel.DataAnnotations;

namespace Hexaplanning.Models
{
    public class QuestUpdateDTO
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int EstimatedTime { get; set; }

        public Guid? ThemeId { get; set; }
        public bool IsPrimaryTheme { get; set; }
        public Guid StatusId { get; set; }
        public int? Advancement { get; set; }

        public HexAssignmentDTO? HexAssignment { get; set; }

        public Guid? QuestGroupId { get; set; }

        public void UpdateQuest(Quest existingQuest)
        {
            existingQuest.Title = Title;
            existingQuest.Description = Description;
            existingQuest.EstimatedTime = EstimatedTime;
            existingQuest.ThemeId = ThemeId;
            existingQuest.IsPrimaryTheme = IsPrimaryTheme;
            existingQuest.StatusId = StatusId;
            existingQuest.Advancement = Advancement;
            existingQuest.HexAssignment = HexAssignment != null ? HexAssignment.ToHexAssignment() : null;
            existingQuest.QuestGroupId = QuestGroupId;
        }

    }
}
