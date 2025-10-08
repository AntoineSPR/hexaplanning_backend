namespace Procrastinator.Models
{
    public class HexAssignmentDTO
    {
        public Guid Id { get; set; }
        public Guid QuestId { get; set; }
        public Guid UserId { get; set; }
        public int Q { get; set; }
        public int R { get; set; }
        public int S { get; set; }

        public HexAssignment ToHexAssignment()
        {
            return new HexAssignment
            {
                Id = Id,
                QuestId = QuestId,
                Q = Q,
                R = R,
                S = S,
            };
        }

        public static HexAssignmentDTO 
            ToHexAssignmentDTO(HexAssignment hexAssignment)
        {
            return new HexAssignmentDTO
            {
                Id = hexAssignment.Id,
                QuestId = hexAssignment.QuestId,
                Q = hexAssignment.Q,
                R = hexAssignment.R,
                S = hexAssignment.S,
            };
        }
    }
}
