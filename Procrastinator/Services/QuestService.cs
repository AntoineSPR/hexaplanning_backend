using Microsoft.EntityFrameworkCore;
using Procrastinator.Context;
using Procrastinator.Models;
using Procrastinator.Utilities;

namespace Procrastinator.Services
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
            var pending_quests = await context.Quests
                .Where(x => x.UserId == userId 
                && x.StatusId != HardCode.STATUS_COMPLETED_ID)
                .ToListAsync();
            return pending_quests.Select(QuestDTO.ToQuestDTO).ToList();
        }
        public async Task<List<QuestDTO>> GetAllCompletedQuestsAsync(Guid userId)
        {
            var completed_quests = await context.Quests
                 .Where(x => x.UserId == userId
                && x.StatusId == HardCode.STATUS_COMPLETED_ID)
                .ToListAsync();
            return completed_quests.Select(QuestDTO.ToQuestDTO).ToList();
        }

        public async Task<List<QuestDTO>> GetAllUnassignedPendingQuestsAsync(Guid userId)
        {
            var unassigned_pending_quests = await context.Quests
                 .Where(x => x.UserId == userId
                && x.StatusId != HardCode.STATUS_COMPLETED_ID && x.HexAssignmentId == null)
                .ToListAsync();
            return unassigned_pending_quests.Select(QuestDTO.ToQuestDTO).ToList();

        }

        public async Task<QuestDTO?> GetQuestByIdAsync(Guid id, Guid userId)
        {
            var quest = await context.Quests.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            return quest == null ? null : QuestDTO.ToQuestDTO(quest);
        }

        public async Task<QuestDTO> CreateQuestAsync(QuestCreateDTO questDto, Guid userId)
        {
            var quest = questDto.ToQuest(userId);
            context.Quests.Add(quest);
            await context.SaveChangesAsync();
            return QuestDTO.ToQuestDTO(quest);
        }

        public async Task<QuestDTO?> UpdateQuestAsync(Guid id, QuestUpdateDTO updatedQuest, Guid userId)
        {
            var quest = await context.Quests.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (quest == null)
            {
                return null;
            }

            updatedQuest.UpdateQuest(quest); // Update the existing quest with new values referenced by 'quest'

            await context.SaveChangesAsync();

            return QuestDTO.ToQuestDTO(quest);
        }

        public async Task<bool> DeleteQuestAsync(Guid id, Guid userId)
        {
            var quest = await context.Quests.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (quest == null)
            {
                return false;
            }
            context.Quests.Remove(quest);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
