using Microsoft.EntityFrameworkCore;
using Procrastinator.Context;
using Procrastinator.Models;

namespace Procrastinator.Services
{
    public class HexAssignmentService(DataContext context)
    {
        private readonly DataContext context = context;

        public async Task<List<HexAssignmentDTO>> GetAllHexAssignmentsAsync()
        {
            var hexAssignments = await context.HexAssignments.ToListAsync();
            return hexAssignments.Select(HexAssignmentDTO.ToHexAssignmentDTO).ToList();
        }

        public async Task<HexAssignmentDTO?> GetHexAssignmentByIdAsync(int id)
        {
            var hexAssignment = await context.HexAssignments.FindAsync(id);
            return hexAssignment == null ? null : HexAssignmentDTO.ToHexAssignmentDTO(hexAssignment);
        }

        public async Task<HexAssignmentDTO> CreateHexAssignmentAsync(HexAssignmentDTO hexAssignmentDto)
        {
            var hexAssignment = hexAssignmentDto.ToHexAssignment();
            context.HexAssignments.Add(hexAssignment);
            await context.SaveChangesAsync();
            return HexAssignmentDTO.ToHexAssignmentDTO(hexAssignment);
        }

        public async Task<HexAssignmentDTO?> UpdateHexAssignmentAsync(int id, HexAssignmentDTO updatedHexAssignment)
        {
            var hexAssignment = await context.HexAssignments.FindAsync(id);
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

        public async Task<bool> DeleteHexAssignmentAsync(int id)
        {
            var hexAssignment = await context.HexAssignments.FindAsync(id);
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
