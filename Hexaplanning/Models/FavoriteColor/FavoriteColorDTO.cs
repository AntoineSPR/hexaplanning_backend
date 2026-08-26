using System.ComponentModel.DataAnnotations;

namespace Hexaplanning.Models
{
    public class FavoriteColorDTO
    {
        public Guid Id { get; set; }
        public string Hex { get; set; }

        public static FavoriteColorDTO ToFavoriteColorDTO(FavoriteColor favoriteColor)
        {
            return new FavoriteColorDTO
            {
                Id = favoriteColor.Id,
                Hex = favoriteColor.Hex
            };
        }
    }

    public class FavoriteColorCreateDTO
    {
        [Required]
        public string Hex { get; set; }

        public FavoriteColor ToFavoriteColor(Guid userId)
        {
            return new FavoriteColor
            {
                Hex = Hex,
                UserId = userId
            };
        }
    }
}
