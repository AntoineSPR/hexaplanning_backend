using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexaplanning.Models
{
    public class Theme : BaseModelOption
    {
        public string Color { get; set; }

        [Required]
        public UserApp User { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public ICollection<Quest> Quests { get; set; } = new List<Quest>();
    }
}
