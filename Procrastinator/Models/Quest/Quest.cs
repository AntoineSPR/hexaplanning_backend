using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Procrastinator.Models
{
    public enum QuestPriority
    {
        PRIMARY,
        SECONDARY,
        TERTIARY,
    }

    public class Quest : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public UserApp User { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public int? Advancement { get; set; }

        public int EstimatedTime { get; set; }

        public Guid PriorityId { get; set; }

        [ForeignKey(nameof(PriorityId))]
        public Priority Priority { get; set; }

        public Guid StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }

        public Guid? HexAssignmentId { get; set; }

        [ForeignKey(nameof(HexAssignmentId))]
        public HexAssignment? HexAssignment { get; set; }
    }
}
