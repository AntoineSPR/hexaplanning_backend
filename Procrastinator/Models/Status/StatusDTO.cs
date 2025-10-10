using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class StatusDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string? Icon { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsArchived { get; set; }

        public static StatusDTO ToStatusDTO(Status status)
        {
            return new StatusDTO
            {
                Id = status.Id,
                Name = status.Name,
                Color = status.Color,
                Icon = status.Icon,
                CreatedAt = status.CreatedAt,
                UpdatedAt = status.UpdatedAt,
                IsArchived = status.IsArchived
            };
        }
    }

    public class StatusCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        public string Color { get; set; }
        
        public string? Icon { get; set; }

        public Status ToStatus()
        {
            return new Status
            {
                Name = Name,
                Color = Color,
                Icon = Icon
            };
        }
    }

    public class StatusUpdateDTO
    {
        [StringLength(100)]
        public string? Name { get; set; }
        
        public string? Color { get; set; }
        
        public string? Icon { get; set; }

        public void UpdateStatus(Status status)
        {
            if (!string.IsNullOrEmpty(Name))
                status.Name = Name;
            if (!string.IsNullOrEmpty(Color))
                status.Color = Color;
            if (Icon != null)
                status.Icon = Icon;
            
            status.UpdatedAt = DateTime.UtcNow;
        }
    }
}