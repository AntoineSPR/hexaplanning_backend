using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public enum QuestPriority
    {
        PRIMARY,
        SECONDARY,
        TERTIARY
    }
    public class Quest
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public UserApp User { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public int EstimatedTime { get; set; }

        [EnumDataType(typeof(QuestPriority))]
        public QuestPriority Priority { get; set; }

        public bool IsDone { get; set; }

        public bool IsAssigned { get; set; }

        public HexAssignment? HexAssignment { get; set; }
    }
}
