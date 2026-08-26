using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hexaplanning.Models
{
    public class FavoriteColor : BaseModel
    {
        public string Hex { get; set; }

        [Required]
        public UserApp User { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
    }
}
