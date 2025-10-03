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
                //UserId = UserId,
                Q = Q,
                R = R,
                S = S,
            };
        }

        public static HexAssignmentDTO ToHexAssignmentDTO(HexAssignment hexAssignment)
        {
            return new HexAssignmentDTO
            {
                Id = hexAssignment.Id,
                QuestId = hexAssignment.QuestId,
                //UserId = hexAssignment.UserId,
                Q = hexAssignment.Q,
                R = hexAssignment.R,
                S = hexAssignment.S,
            };
        }
    }
}
