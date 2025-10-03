using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Procrastinator.Models
{
    public class HexAssignment : BaseModel
    {
        [Required]
        public int Q { get; set; }
        [Required]
        public int R { get; set; }
        [Required]
        public int S { get; set; }
        [Required]
        public Guid QuestId { get; set; }
        [Required]
        [ForeignKey(nameof(QuestId))]
        public Quest Quest { get; set; }
        [Required]
        //public UserApp User { get; set; }
        //[ForeignKey(nameof(User))]
        //public Guid UserId { get; set; }
    }
}
