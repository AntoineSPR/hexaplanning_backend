using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Procrastinator.Models
{
    public class QuestDTO
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public int ExperienceGain { get; set; }

        public int Apprehension { get; set; }

        public int EstimatedTime { get; set; }

        public int Difficulty { get; set; }

        [EnumDataType(typeof(QuestPriority))]
        public QuestPriority Priority { get; set; }

        public bool IsDone { get; set; }

        public bool IsRepeatable { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }


        public Quest ToQuest()
        {
            return new Quest
            {
                Title = Title,
                Description = Description,
                UserId = UserId,
                ExperienceGain = ExperienceGain,
                Apprehension = Apprehension,
                EstimatedTime = EstimatedTime,
                Difficulty = Difficulty,
                Priority = Priority,
                IsDone = IsDone,
                IsRepeatable = IsRepeatable,
                StartDate = StartDate,
                EndDate = EndDate
            };
        }

        public static QuestDTO ToQuestDTO(Quest quest)
        {
            return new QuestDTO
            {
                Id = quest.Id,
                Title = quest.Title,
                Description = quest.Description,
                UserId = quest.UserId,
                ExperienceGain = quest.ExperienceGain,
                Apprehension = quest.Apprehension,
                EstimatedTime = quest.EstimatedTime,
                Difficulty = quest.Difficulty,
                Priority = quest.Priority,
                IsDone = quest.IsDone,
                IsRepeatable = quest.IsRepeatable,
                StartDate = quest.StartDate,
                EndDate = quest.EndDate
            };

        }
        
    }
}
