namespace Procrastinator.Models
{
    public class HexAssignment
    {
        public int Id { get; set; }
        public string? QuestId { get; set; }
        public int Q { get; set; }
        public int R { get; set; }
        public int S { get; set; }
        public Quest? Quest { get; set; }
    }
}
