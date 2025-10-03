using Microsoft.EntityFrameworkCore;
using Procrastinator.Context;
using Procrastinator.Models;

namespace Procrastinator.Services
{
    public class HexAssignmentService(DataContext context)
    {
        private readonly DataContext context = context;

        public async Task<List<HexAssignmentDTO>> GetAllHexAssignmentsAsync(Guid userId)
        {
            var hexAssignments = await context.HexAssignments.Include(x => x.Quest).Where(x => x.Quest.UserId == userId).ToListAsync();
            return hexAssignments.Select(HexAssignmentDTO.ToHexAssignmentDTO).ToList();
        }

        public async Task<HexAssignmentDTO?> GetHexAssignmentByIdAsync(Guid id, Guid userId)
        {
            var hexAssignment = await context.HexAssignments.Include(x => x.Quest).FirstOrDefaultAsync(x => x.Quest.UserId == userId && x.Id == id);
            return hexAssignment == null ? null : HexAssignmentDTO.ToHexAssignmentDTO(hexAssignment);
        }

        public async Task<HexAssignmentDTO?> GetHexAssignmentByQuestIdAsync(Guid questId, Guid userId)
        {
            var hexAssignement = await context.HexAssignments.Include(h => h.Quest).FirstOrDefaultAsync(h => h.QuestId == questId && h.Quest.UserId == userId);
            return hexAssignement == null ? null : HexAssignmentDTO.ToHexAssignmentDTO(hexAssignement);

        }

        public async Task<HexAssignmentDTO?> GetHexAssignmentByCoordinatesAsync(int q, int r, int s, Guid userId)
        {
            var hexAssignment = await context.HexAssignments.Include(x => x.Quest).Where(x => x.Quest.UserId == userId).FirstOrDefaultAsync(h => h.Q == q && h.R == r && h.S == s);
            return hexAssignment == null ? null : HexAssignmentDTO.ToHexAssignmentDTO(hexAssignment);
        }

        public async Task<HexAssignmentDTO> CreateHexAssignmentAsync(HexAssignmentDTO hexAssignmentDto, Guid userId)
        {
            hexAssignmentDto.UserId = userId;
            var hexAssignment = hexAssignmentDto.ToHexAssignment();
            context.HexAssignments.Add(hexAssignment);
            await context.SaveChangesAsync();
            return HexAssignmentDTO.ToHexAssignmentDTO(hexAssignment);
        }

        public async Task<HexAssignmentDTO?> UpdateHexAssignmentAsync(Guid id, HexAssignmentDTO updatedHexAssignment, Guid userId)
        {
            var hexAssignment = await context.HexAssignments.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (hexAssignment == null)
            {
                return null;
            }
            hexAssignment.QuestId = updatedHexAssignment.QuestId;
            hexAssignment.Q = updatedHexAssignment.Q;
            hexAssignment.R = updatedHexAssignment.R;
            hexAssignment.S = updatedHexAssignment.S;
            await context.SaveChangesAsync();
            return HexAssignmentDTO.ToHexAssignmentDTO(hexAssignment);
        }

        public async Task<bool> DeleteHexAssignmentAsync(int q, int r, int s, Guid userId)
        {
            var hexAssignment = await context.HexAssignments.FirstOrDefaultAsync(h => h.Q == q && h.R == r && h.S == s && h.UserId == userId);
            if (hexAssignment == null)
            {
                return false;
            }
            context.HexAssignments.Remove(hexAssignment);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
