using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procrastinator.Models;
using Procrastinator.Services;
using Procrastinator.Utilities;

namespace Procrastinator.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    [CheckUser]
    public class ThemeController : ControllerBase
    {
        private readonly ThemeService themeService;
        public ThemeController(ThemeService themeService)
        {
            this.themeService = themeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllThemes()
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var themes = await themeService.GetAllThemesAsync(userId);
                return Ok(themes);
            }
            return Unauthorized();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetThemeById(Guid id)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var theme = await themeService.GetThemeByIdAsync(id, userId);
                if (theme == null)
                {
                    return NotFound();
                }
                return Ok(theme);
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTheme([FromBody] ThemeCreateDTO themeDto)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var createdTheme = await themeService.CreateThemeAsync(themeDto, userId);
                return CreatedAtAction(nameof(GetThemeById), new { id = createdTheme.Id }, createdTheme);
            }
            return Unauthorized();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTheme(Guid id, [FromBody] ThemeUpdateDTO updatedTheme)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var theme = await themeService.UpdateThemeAsync(id, updatedTheme, userId);
                if (theme == null)
                {
                    return NotFound();
                }
                return Ok(theme);
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheme(Guid id)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var result = await themeService.DeleteThemeAsync(id, userId);
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
