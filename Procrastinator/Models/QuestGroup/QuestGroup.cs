using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Procrastinator.Models
{
    public class QuestGroup : BaseModelOption
    {
        [Required]
        public UserApp User { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public ICollection<Quest> Quests { get; set; } = new List<Quest>();
    }
}
