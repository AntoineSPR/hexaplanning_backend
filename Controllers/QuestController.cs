using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Procrastinator.Models;
using Procrastinator.Services;

namespace Procrastinator.Controllers
{
    [Route("[controller]")]
    //TODO : Change
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

        [HttpGet("pending")]
        public async Task<IActionResult> GetAllPendingQuests()
        {
            var pending_quests = await questService.GetAllPendingQuestsAsync();
            return Ok(pending_quests);
        }

        [HttpGet("completed")]
        public async Task<IActionResult> GetAllCompletedQuests()
        {
            var completed_quests = await questService.GetAllCompletedQuestsAsync();
            return Ok(completed_quests);
        }

        [HttpGet("unassigned_pending")]
        public async Task<IActionResult> GetAllUnassignedPendingQuests()
        {
            var unassigned_pending_quests = await questService.GetAllUnassignedPendingQuestsAsync();
            return Ok(unassigned_pending_quests);
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
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                //TODO : Change
                //return Unauthorized();
                userId = "d66a44af-736a-4788-8ba2-9c6aa7c29e2e";
            }
            questDto.UserId = userId;

            try
            {
                var createdQuest = await questService.CreateQuestAsync(questDto);
                return CreatedAtAction(nameof(GetQuestById), new { id = createdQuest.Id }, createdQuest);
            } catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                return Conflict(new { message = "Une quête est déjà associée à cet hexagone" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuest(Guid id, [FromBody] QuestDTO updatedQuest)
        {
            try
            {
                var quest = await questService.UpdateQuestAsync(id, updatedQuest);
                if (quest == null)
                {
                    return NotFound();
                }
                return Ok(quest);
            } catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                return Conflict(new { message = "Une quête est déjà associée à cet hexagone" });
            }
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
