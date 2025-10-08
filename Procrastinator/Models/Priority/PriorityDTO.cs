using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class PriorityDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string? Icon { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsArchived { get; set; }

        public static PriorityDTO ToPriorityDTO(Priority priority)
        {
            return new PriorityDTO
            {
                Id = priority.Id,
                Name = priority.Name,
                Color = priority.Color,
                Icon = priority.Icon,
                CreatedAt = priority.CreatedAt,
                UpdatedAt = priority.UpdatedAt,
                IsArchived = priority.IsArchived
            };
        }
    }

    public class PriorityCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        public string Color { get; set; }
        
        public string? Icon { get; set; }

        public Priority ToPriority()
        {
            return new Priority
            {
                Name = Name,
                Color = Color,
                Icon = Icon
            };
        }
    }

    public class PriorityUpdateDTO
    {
        [StringLength(100)]
        public string? Name { get; set; }
        
        public string? Color { get; set; }
        
        public string? Icon { get; set; }

        public void UpdatePriority(Priority priority)
        {
            if (!string.IsNullOrEmpty(Name))
                priority.Name = Name;
            if (!string.IsNullOrEmpty(Color))
                priority.Color = Color;
            if (Icon != null)
                priority.Icon = Icon;
            
            priority.UpdatedAt = DateTime.UtcNow;
        }
    }
}