using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procrastinator.Models;
using Procrastinator.Services;

namespace Procrastinator.Controllers
{
    [Route("[controller]")]
    //[Authorize]
    [ApiController]
    public class QuestController: ControllerBase
    {
        private readonly QuestService questService;
        public QuestController(QuestService questService)
        {
            this.questService = questService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuests()
        {
            var quests = await questService.GetAllQuestsAsync();
            return Ok(quests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestById(Guid id)
        {
            var quest = await questService.GetQuestByIdAsync(id);
            if (quest == null)
            {
                return NotFound();
            }
            return Ok(quest);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuest([FromBody] QuestDTO questDto)
        {
            var createdQuest = await questService.CreateQuestAsync(questDto);
            return CreatedAtAction(nameof(GetQuestById), new { id = createdQuest.Id }, createdQuest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuest(Guid id, [FromBody] QuestDTO updatedQuest)
        {
            var quest = await questService.UpdateQuestAsync(id, updatedQuest);
            if (quest == null)
            {
                return NotFound();
            }
            return Ok(quest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuest(Guid id)
        {
            var result = await questService.DeleteQuestAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
