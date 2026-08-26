using Microsoft.EntityFrameworkCore;
using Hexaplanning.Context;
using Hexaplanning.Models;
using Hexaplanning.Utilities;

namespace Hexaplanning.Services
{
    public class QuestService(DataContext context)
    {
        private readonly DataContext context = context;

        public async Task<List<QuestDTO>> GetAllQuestsAsync(Guid userId)
        {
            var quests = await context.Quests.Where(x => x.UserId == userId).ToListAsync();
            return quests.Select(QuestDTO.ToQuestDTO).ToList();
        }

        public async Task<List<QuestDTO>> GetAllPendingQuestsAsync(Guid userId)
        {
            var pending_quests = await context
                .Quests.Where(x => x.UserId == userId && x.StatusId != HardCode.STATUS_COMPLETED_ID)
                .ToListAsync();
            return pending_quests.Select(QuestDTO.ToQuestDTO).ToList();
        }

        public async Task<List<QuestDTO>> GetAllCompletedQuestsAsync(Guid userId)
        {
            var completed_quests = await context
                .Quests.Where(x => x.UserId == userId && x.StatusId == HardCode.STATUS_COMPLETED_ID)
                .ToListAsync();
            return completed_quests.Select(QuestDTO.ToQuestDTO).ToList();
        }

        public async Task<List<QuestDTO>> GetAllUnassignedPendingQuestsAsync(Guid userId)
        {
            var unassigned_pending_quests = await context
                .Quests
                .Include(q => q.HexAssignment)
                .Where(x =>
                    x.UserId == userId
                    && x.StatusId != HardCode.STATUS_COMPLETED_ID
                    && x.HexAssignment == null
                )
                .ToListAsync();
            return unassigned_pending_quests.Select(QuestDTO.ToQuestDTO).ToList();
        }

        public async Task<QuestDTO?> GetQuestByIdAsync(Guid id, Guid userId)
        {
            var quest = await context.Quests.FirstOrDefaultAsync(x =>
                x.Id == id && x.UserId == userId
            );
            return quest == null ? null : QuestDTO.ToQuestDTO(quest);
        }

        public async Task<QuestDTO> CreateQuestAsync(QuestCreateDTO questDto, Guid userId)
        {
            var quest = questDto.ToQuest(userId);
            context.Quests.Add(quest);
            await context.SaveChangesAsync();
            return QuestDTO.ToQuestDTO(quest);
        }

        public async Task<QuestDTO?> UpdateQuestAsync(
            Guid id,
            QuestUpdateDTO updatedQuest,
            Guid userId
        )
        {
            var quest = await context.Quests.FirstOrDefaultAsync(x =>
                x.Id == id && x.UserId == userId
            );
            if (quest == null)
            {
                return null;
            }

            var previousGroupId = quest.QuestGroupId;

            updatedQuest.UpdateQuest(quest); // Update the existing quest with new values referenced by 'quest'

            await context.SaveChangesAsync();

            // Whether this quest left its group (cleared) or moved to a different one, the group
            // it left behind is deleted outright once it has no members left, rather than
            // lingering as an empty row - the same cleanup CreateQuestGroupAsync does when it
            // reassigns quests away from their previous group.
            if (previousGroupId.HasValue && previousGroupId != quest.QuestGroupId)
            {
                await DeleteGroupIfEmptyAsync(previousGroupId.Value);
            }

            return QuestDTO.ToQuestDTO(quest);
        }

        // A group that's lost its last member no longer has any reason to exist - see the two
        // call sites above/in QuestGroupService.CreateQuestGroupAsync for why a quest's group can
        // change out from under it without going through QuestGroupService.DeleteQuestGroupAsync's
        // own explicit-delete path.
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

        public async Task<bool> DeleteQuestAsync(Guid id, Guid userId)
        {
            var quest = await context.Quests.FirstOrDefaultAsync(x =>
                x.Id == id && x.UserId == userId
            );
            if (quest == null)
            {
                return false;
            }

            var assignment = await context.HexAssignments.FirstOrDefaultAsync(a =>
                a.QuestId == quest.Id
            );

            if (assignment is not null)
            {
                context.HexAssignments.Remove(assignment);
            }

            context.Quests.Remove(quest);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
