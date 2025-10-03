using Procrastinator.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class BaseModel : IArchivable, ICreatable, IUpdatable
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsArchived { get; set; }
    }
}
