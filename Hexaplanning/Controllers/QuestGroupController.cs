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
    public class QuestGroupController : ControllerBase
    {
        private readonly QuestGroupService questGroupService;
        public QuestGroupController(QuestGroupService questGroupService)
        {
            this.questGroupService = questGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestGroups()
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var questGroups = await questGroupService.GetAllQuestGroupsAsync(userId);
                return Ok(questGroups);
            }
            return Unauthorized();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestGroupById(Guid id)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var questGroup = await questGroupService.GetQuestGroupByIdAsync(id, userId);
                if (questGroup == null)
                {
                    return NotFound();
                }
                return Ok(questGroup);
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestGroup([FromBody] QuestGroupCreateDTO questGroupDto)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var createdQuestGroup = await questGroupService.CreateQuestGroupAsync(questGroupDto, userId);
                return CreatedAtAction(nameof(GetQuestGroupById), new { id = createdQuestGroup.Id }, createdQuestGroup);
            }
            return Unauthorized();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestGroup(Guid id, [FromBody] QuestGroupUpdateDTO updatedQuestGroup)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var questGroup = await questGroupService.UpdateQuestGroupAsync(id, updatedQuestGroup, userId);
                if (questGroup == null)
                {
                    return NotFound();
                }
                return Ok(questGroup);
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestGroup(Guid id)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var result = await questGroupService.DeleteQuestGroupAsync(id, userId);
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
