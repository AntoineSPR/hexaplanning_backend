namespace Procrastinator.Models
{
    public class ThemeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public List<Guid> QuestIds { get; set; } = new List<Guid>();

        public static ThemeDTO ToThemeDTO(Theme theme)
        {
            return new ThemeDTO
            {
                Id = theme.Id,
                Name = theme.Name,
                Color = theme.Color,
                QuestIds = theme.Quests.Select(q => q.Id).ToList()
            };
        }
    }
}
