using Microsoft.EntityFrameworkCore;
using Procrastinator.Context;
using Procrastinator.Models;

namespace Procrastinator.Services
{
    public class StatusService(DataContext context)
    {
        private readonly DataContext context = context;

        public async Task<List<StatusDTO>> GetAllStatusesAsync()
        {
            var statuses = await context.Statuses
                .Where(s => !s.IsArchived)
                .OrderBy(s => s.Name)
                .ToListAsync();
            return statuses.Select(StatusDTO.ToStatusDTO).ToList();
        }

        public async Task<StatusDTO?> GetStatusByIdAsync(Guid id)
        {
            var status = await context.Statuses
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsArchived);
            return status == null ? null : StatusDTO.ToStatusDTO(status);
        }

        public async Task<StatusDTO> CreateStatusAsync(StatusCreateDTO statusDto)
        {
            var status = statusDto.ToStatus();
            context.Statuses.Add(status);
            await context.SaveChangesAsync();
            return StatusDTO.ToStatusDTO(status);
        }

        public async Task<StatusDTO?> UpdateStatusAsync(Guid id, StatusUpdateDTO updatedStatus)
        {
            var status = await context.Statuses
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsArchived);
            if (status == null)
            {
                return null;
            }

            updatedStatus.UpdateStatus(status);
            await context.SaveChangesAsync();

            return StatusDTO.ToStatusDTO(status);
        }

        public async Task<bool> DeleteStatusAsync(Guid id)
        {
            var status = await context.Statuses
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsArchived);
            if (status == null)
            {
                return false;
            }

            // Check if status is used by any quests
            var questsUsingStatus = await context.Quests
                .AnyAsync(q => q.StatusId == id && !q.IsArchived);
            
            if (questsUsingStatus)
            {
                // Archive instead of delete if it's being used
                status.IsArchived = true;
                status.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                // Physically delete if not used
                context.Statuses.Remove(status);
            }
            
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ArchiveStatusAsync(Guid id)
        {
            var status = await context.Statuses
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsArchived);
            if (status == null)
            {
                return false;
            }

            status.IsArchived = true;
            status.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreStatusAsync(Guid id)
        {
            var status = await context.Statuses
                .FirstOrDefaultAsync(s => s.Id == id && s.IsArchived);
            if (status == null)
            {
                return false;
            }

            status.IsArchived = false;
            status.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }
    }
}