using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Procrastinator.Services;

namespace Procrastinator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixturesController : ControllerBase
    {
        private readonly FixturesService fixturesService;

        public FixturesController(FixturesService fixturesService)
        {
            this.fixturesService = fixturesService;
        }

        [HttpPost("create-users/{number}")]
        public IActionResult CreateUsers(int number)
        {
            fixturesService.CreateUsers(number);
            return Ok($"Created {number} users.");
        }

        [HttpPost("create-quests/{number}")]
        public async Task<IActionResult> CreateQuests(int number)
        {
            await fixturesService.CreateQuests(number);
            return Ok($"Created {number} quests for each user.");
        }
    }
}
