using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hexaplanning.Models;
using Hexaplanning.Services;
using Hexaplanning.Utilities;

namespace Hexaplanning.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    [CheckUser]
    public class FavoriteColorController : ControllerBase
    {
        private readonly FavoriteColorService favoriteColorService;
        public FavoriteColorController(FavoriteColorService favoriteColorService)
        {
            this.favoriteColorService = favoriteColorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFavoriteColors()
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var favoriteColors = await favoriteColorService.GetAllFavoriteColorsAsync(userId);
                return Ok(favoriteColors);
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavoriteColor([FromBody] FavoriteColorCreateDTO favoriteColorDto)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var createdFavoriteColor = await favoriteColorService.CreateFavoriteColorAsync(favoriteColorDto, userId);
                return Ok(createdFavoriteColor);
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoriteColor(Guid id)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var result = await favoriteColorService.DeleteFavoriteColorAsync(id, userId);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            return Unauthorized();
        }
    }
}
