using Microsoft.EntityFrameworkCore;
using Hexaplanning.Context;
using Hexaplanning.Models;

namespace Hexaplanning.Services
{
    public class QuestGroupService(DataContext context)
    {
        private readonly DataContext context = context;

        public async Task<List<QuestGroupDTO>> GetAllQuestGroupsAsync(Guid userId)
        {
            var groups = await context.QuestGroups
                .Include(g => g.Quests)
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return groups.Select(QuestGroupDTO.ToQuestGroupDTO).ToList();
        }

        public async Task<QuestGroupDTO?> GetQuestGroupByIdAsync(Guid id, Guid userId)
        {
            var group = await context.QuestGroups
                .Include(g => g.Quests)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            return group == null ? null : QuestGroupDTO.ToQuestGroupDTO(group);
        }

        public async Task<QuestGroupDTO> CreateQuestGroupAsync(QuestGroupCreateDTO dto, Guid userId)
        {
            var group = dto.ToQuestGroup(userId);
            context.QuestGroups.Add(group);

            var quests = await context.Quests
                .Where(q => dto.QuestIds.Contains(q.Id) && q.UserId == userId)
                .ToListAsync();

            // The frontend's flood-fill already excludes any quest that's already in a group (see
            // QuestGroupGeometryService.floodFillOccupiedCluster), so this should rarely fire in
            // practice - kept as a server-side backstop rather than trusting that exclusion alone,
            // since this is the source of truth for group membership.
            var previousGroupIds = quests
                .Where(q => q.QuestGroupId.HasValue)
                .Select(q => q.QuestGroupId!.Value)
                .Distinct()
                .ToList();

            foreach (var q in quests)
            {
                q.QuestGroupId = group.Id;
            }

            await context.SaveChangesAsync();

            foreach (var previousGroupId in previousGroupIds)
            {
                await DeleteGroupIfEmptyAsync(previousGroupId);
            }

            group.Quests = quests;
            return QuestGroupDTO.ToQuestGroupDTO(group);
        }

        public async Task<QuestGroupDTO?> UpdateQuestGroupAsync(Guid id, QuestGroupUpdateDTO dto, Guid userId)
        {
            var group = await context.QuestGroups
                .Include(g => g.Quests)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (group == null)
            {
                return null;
            }

            dto.UpdateQuestGroup(group);

            await context.SaveChangesAsync();

            return QuestGroupDTO.ToQuestGroupDTO(group);
        }

        public async Task<bool> DeleteQuestGroupAsync(Guid id, Guid userId)
        {
            var group = await context.QuestGroups.FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);
            if (group == null)
            {
                return false;
            }

            var members = await context.Quests.Where(q => q.QuestGroupId == id).ToListAsync();
            foreach (var q in members)
            {
                q.QuestGroupId = null;
            }

            context.QuestGroups.Remove(group);
            await context.SaveChangesAsync();
            return true;
        }

        // Same helper as QuestService's - duplicated rather than shared, matching this codebase's
        // existing style of each service querying DataContext directly rather than composing
        // through other services.
        private async Task DeleteGroupIfEmptyAsync(Guid groupId)
        {
            var stillHasMembers = await context.Quests.AnyAsync(q => q.QuestGroupId == groupId);
            if (stillHasMembers)
            {
                return;
            }

            var group = await context.QuestGroups.FindAsync(groupId);
            if (group == null)
            {
                return;
            }

            context.QuestGroups.Remove(group);
            await context.SaveChangesAsync();
        }
    }
}
