using Microsoft.EntityFrameworkCore;
using Procrastinator.Context;
using Procrastinator.Models;

namespace Procrastinator.Services
{
    public class ThemeService(DataContext context)
    {
        private readonly DataContext context = context;

        public async Task<List<ThemeDTO>> GetAllThemesAsync(Guid userId)
        {
            var themes = await context.Themes
                .Include(t => t.Quests)
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return themes.Select(ThemeDTO.ToThemeDTO).ToList();
        }

        public async Task<ThemeDTO?> GetThemeByIdAsync(Guid id, Guid userId)
        {
            var theme = await context.Themes
                .Include(t => t.Quests)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            return theme == null ? null : ThemeDTO.ToThemeDTO(theme);
        }

        public async Task<ThemeDTO> CreateThemeAsync(ThemeCreateDTO dto, Guid userId)
        {
            var theme = dto.ToTheme(userId);
            context.Themes.Add(theme);
            await context.SaveChangesAsync();
            return ThemeDTO.ToThemeDTO(theme);
        }

        public async Task<ThemeDTO?> UpdateThemeAsync(Guid id, ThemeUpdateDTO dto, Guid userId)
        {
            var theme = await context.Themes
                .Include(t => t.Quests)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (theme == null)
            {
                return null;
            }

            dto.UpdateTheme(theme);

            await context.SaveChangesAsync();

            return ThemeDTO.ToThemeDTO(theme);
        }

        public async Task<bool> DeleteThemeAsync(Guid id, Guid userId)
        {
            var theme = await context.Themes.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (theme == null)
            {
                return false;
            }

            // The FK has no DB-level cascade/set-null configured, so member quests must be
            // detached before the row is removed or SaveChangesAsync throws on the constraint -
            // same pattern as QuestGroupService.DeleteQuestGroupAsync. Unlike a QuestGroup, a
            // Theme is never auto-deleted when it becomes empty (it's a user-named category worth
            // keeping around with no members), so this is the only place membership gets cleared.
            var members = await context.Quests.Where(q => q.ThemeId == id).ToListAsync();
            foreach (var q in members)
            {
                q.ThemeId = null;
                q.IsPrimaryTheme = false;
            }

            context.Themes.Remove(theme);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
