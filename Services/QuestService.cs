using Microsoft.EntityFrameworkCore;
using Procrastinator.Context;
using Procrastinator.Models;

namespace Procrastinator.Services
{
    public class QuestService(DataContext context)
    {
        private readonly DataContext context = context;

        public async Task<List<QuestDTO>> GetAllQuestsAsync()
        {
            var quests = await context.Quests.ToListAsync();
            return quests.Select(QuestDTO.ToQuestDTO).ToList();
        }

        public async Task<List<QuestDTO>> GetAllPendingQuestsAsync()
        {
            var pending_quests = await context.Quests
                .Where(q => q.IsDone == false)
                .ToListAsync();
            return pending_quests.Select(QuestDTO.ToQuestDTO).ToList();
        }
        public async Task<List<QuestDTO>> GetAllCompletedQuestsAsync()
        {
            var completed_quests = await context.Quests
                .Where(q => q.IsDone == true)
                .ToListAsync();
            return completed_quests.Select(QuestDTO.ToQuestDTO).ToList();
        }

        public async Task<List<QuestDTO>> GetAllUnassignedPendingQuestsAsync()
        {
            var unassigned_pending_quests = await context.Quests
                .Where(q => q.IsAssigned == false && q.IsDone == false)
                .ToListAsync();
            return unassigned_pending_quests.Select(QuestDTO.ToQuestDTO).ToList();

        }

        public async Task<QuestDTO?> GetQuestByIdAsync(Guid id)
        {
            var quest = await context.Quests.FindAsync(id);
            return quest == null ? null : QuestDTO.ToQuestDTO(quest);
        }

        public async Task<QuestDTO> CreateQuestAsync(QuestDTO questDto)
        {
            var quest = questDto.ToQuest();
            context.Quests.Add(quest);
            await context.SaveChangesAsync();
            return QuestDTO.ToQuestDTO(quest);
        }

        public async Task<QuestDTO?> UpdateQuestAsync(Guid id, QuestDTO updatedQuest)
        {
            var quest = await context.Quests.FindAsync(id);
            if (quest == null)
            {
                return null;
            }
            quest.Title = updatedQuest.Title;
            quest.Description = updatedQuest.Description;
            quest.ExperienceGain = updatedQuest.ExperienceGain;
            quest.Apprehension = updatedQuest.Apprehension;
            quest.EstimatedTime = updatedQuest.EstimatedTime;
            quest.Difficulty = updatedQuest.Difficulty;
            quest.Priority = updatedQuest.Priority;
            quest.IsDone = updatedQuest.IsDone;
            quest.IsRepeatable = updatedQuest.IsRepeatable;
            quest.IsAssigned = updatedQuest.IsAssigned;
            await context.SaveChangesAsync();
            return QuestDTO.ToQuestDTO(quest);
        }

        public async Task<bool> DeleteQuestAsync(Guid id)
        {
            var quest = await context.Quests.FindAsync(id);
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
