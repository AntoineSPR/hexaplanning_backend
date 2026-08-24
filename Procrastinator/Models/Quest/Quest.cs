using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Procrastinator.Models
{
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

        public Guid? ThemeId { get; set; }

        [ForeignKey(nameof(ThemeId))]
        public Theme? Theme { get; set; }

        public bool IsPrimaryTheme { get; set; }

        public Guid StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }

        public HexAssignment? HexAssignment { get; set; }

        public Guid? QuestGroupId { get; set; }

        [ForeignKey(nameof(QuestGroupId))]
        public QuestGroup? QuestGroup { get; set; }
    }
}
