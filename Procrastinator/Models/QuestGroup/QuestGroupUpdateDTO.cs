using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class QuestGroupUpdateDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        public void UpdateQuestGroup(QuestGroup existing)
        {
            existing.Name = Name;
            existing.Color = Color;
        }
    }
}
