using Microsoft.EntityFrameworkCore;
using Procrastinator.Context;
using Procrastinator.Models;

namespace Procrastinator.Services
{
    public class PriorityService(DataContext context)
    {
        private readonly DataContext context = context;

        public async Task<List<PriorityDTO>> GetAllPrioritiesAsync()
        {
            var priorities = await context.Priorities
                .Where(p => !p.IsArchived)
                .OrderBy(p => p.Name)
                .ToListAsync();
            return priorities.Select(PriorityDTO.ToPriorityDTO).ToList();
        }

        public async Task<PriorityDTO?> GetPriorityByIdAsync(Guid id)
        {
            var priority = await context.Priorities
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsArchived);
            return priority == null ? null : PriorityDTO.ToPriorityDTO(priority);
        }

        public async Task<PriorityDTO> CreatePriorityAsync(PriorityCreateDTO priorityDto)
        {
            var priority = priorityDto.ToPriority();
            context.Priorities.Add(priority);
            await context.SaveChangesAsync();
            return PriorityDTO.ToPriorityDTO(priority);
        }

        public async Task<PriorityDTO?> UpdatePriorityAsync(Guid id, PriorityUpdateDTO updatedPriority)
        {
            var priority = await context.Priorities
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsArchived);
            if (priority == null)
            {
                return null;
            }

            updatedPriority.UpdatePriority(priority);
            await context.SaveChangesAsync();

            return PriorityDTO.ToPriorityDTO(priority);
        }

        public async Task<bool> DeletePriorityAsync(Guid id)
        {
            var priority = await context.Priorities
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsArchived);
            if (priority == null)
            {
                return false;
            }

            // Check if priority is used by any quests
            var questsUsingPriority = await context.Quests
                .AnyAsync(q => q.PriorityId == id && !q.IsArchived);
            
            if (questsUsingPriority)
            {
                // Archive instead of delete if it's being used
                priority.IsArchived = true;
                priority.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                // Physically delete if not used
                context.Priorities.Remove(priority);
            }
            
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ArchivePriorityAsync(Guid id)
        {
            var priority = await context.Priorities
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsArchived);
            if (priority == null)
            {
                return false;
            }

            priority.IsArchived = true;
            priority.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestorePriorityAsync(Guid id)
        {
            var priority = await context.Priorities
                .FirstOrDefaultAsync(p => p.Id == id && p.IsArchived);
            if (priority == null)
            {
                return false;
            }

            priority.IsArchived = false;
            priority.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }
    }
}