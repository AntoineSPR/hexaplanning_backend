namespace Hexaplanning.Models
{
    public class QuestGroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public List<Guid> QuestIds { get; set; } = new List<Guid>();

        public static QuestGroupDTO ToQuestGroupDTO(QuestGroup group)
        {
            return new QuestGroupDTO
            {
                Id = group.Id,
                Name = group.Name,
                Color = group.Color,
                QuestIds = group.Quests.Select(q => q.Id).ToList()
            };
        }
    }
}
