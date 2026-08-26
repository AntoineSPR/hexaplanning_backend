using Microsoft.EntityFrameworkCore;
using Hexaplanning.Context;
using Hexaplanning.Models;

namespace Hexaplanning.Services
{
    public class FavoriteColorService(DataContext context)
    {
        private readonly DataContext context = context;

        public async Task<List<FavoriteColorDTO>> GetAllFavoriteColorsAsync(Guid userId)
        {
            var favoriteColors = await context.FavoriteColors
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return favoriteColors.Select(FavoriteColorDTO.ToFavoriteColorDTO).ToList();
        }

        public async Task<FavoriteColorDTO> CreateFavoriteColorAsync(FavoriteColorCreateDTO dto, Guid userId)
        {
            // Avoids duplicate rows if the same color gets starred twice (e.g. from both the
            // group and theme modals, or a double click) - the frontend also guards against this
            // client-side, but this is the source of truth.
            var existing = await context.FavoriteColors
                .FirstOrDefaultAsync(f => f.UserId == userId && f.Hex.ToLower() == dto.Hex.ToLower());
            if (existing != null)
            {
                return FavoriteColorDTO.ToFavoriteColorDTO(existing);
            }

            var favoriteColor = dto.ToFavoriteColor(userId);
            context.FavoriteColors.Add(favoriteColor);
            await context.SaveChangesAsync();
            return FavoriteColorDTO.ToFavoriteColorDTO(favoriteColor);
        }

        public async Task<bool> DeleteFavoriteColorAsync(Guid id, Guid userId)
        {
            var favoriteColor = await context.FavoriteColors.FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);
            if (favoriteColor == null)
            {
                return false;
            }

            context.FavoriteColors.Remove(favoriteColor);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
