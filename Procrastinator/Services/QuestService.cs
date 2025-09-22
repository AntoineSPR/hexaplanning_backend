using Microsoft.EntityFrameworkCore;
using Procrastinator.Context;
using Procrastinator.Models;

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
                .Where(x => x.UserId == userId)
                .Where(q => q.IsDone == false)
                .ToListAsync();
            return pending_quests.Select(QuestDTO.ToQuestDTO).ToList();
        }
        public async Task<List<QuestDTO>> GetAllCompletedQuestsAsync(Guid userId)
        {
            var completed_quests = await context.Quests
                .Where(x => x.UserId == userId)
                .Where(q => q.IsDone == true)
                .ToListAsync();
            return completed_quests.Select(QuestDTO.ToQuestDTO).ToList();
        }

        public async Task<List<QuestDTO>> GetAllUnassignedPendingQuestsAsync(Guid userId)
        {
            var unassigned_pending_quests = await context.Quests
                .Where(x => x.UserId == userId)
                .Where(q => q.IsAssigned == false && q.IsDone == false)
                .ToListAsync();
            return unassigned_pending_quests.Select(QuestDTO.ToQuestDTO).ToList();

        }

        public async Task<QuestDTO?> GetQuestByIdAsync(Guid id, Guid userId)
        {
            var quest = await context.Quests.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            return quest == null ? null : QuestDTO.ToQuestDTO(quest);
        }

        public async Task<QuestDTO> CreateQuestAsync(QuestDTO questDto, Guid userId)
        {
            questDto.UserId = userId;
            var quest = questDto.ToQuest();
            context.Quests.Add(quest);
            await context.SaveChangesAsync();
            return QuestDTO.ToQuestDTO(quest);
        }

        public async Task<QuestDTO?> UpdateQuestAsync(Guid id, QuestDTO updatedQuest, Guid userId)
        {
            var quest = await context.Quests.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (quest == null)
            {
                return null;
            }
            quest.Title = updatedQuest.Title;
            quest.Description = updatedQuest.Description;
            quest.EstimatedTime = updatedQuest.EstimatedTime;
            quest.Priority = updatedQuest.Priority;
            quest.IsDone = updatedQuest.IsDone;
            quest.IsAssigned = updatedQuest.IsAssigned;
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
