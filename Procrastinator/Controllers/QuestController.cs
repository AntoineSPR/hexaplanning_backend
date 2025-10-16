using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Procrastinator.Models;
using Procrastinator.Services;
using Procrastinator.Utilities;

namespace Procrastinator.Controllers
{
    /// <summary>
    /// Gestion des quêtes
    /// </summary>
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    [CheckUser]
    public class QuestController : ControllerBase
    {
        private readonly QuestService questService;
        public QuestController(QuestService questService)
        {
            this.questService = questService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuests()
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var quests = await questService.GetAllQuestsAsync(userId);
                return Ok(quests);
            }
            return Unauthorized();
        }

        /// <summary>
        /// Ceci est un test
        /// </summary>
        /// <returns></returns>
        [HttpGet("pending")]
        public async Task<IActionResult> GetAllPendingQuests()
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var pending_quests = await questService.GetAllPendingQuestsAsync(userId);
                return Ok(pending_quests);
            }
            return Unauthorized();
        }

        [HttpGet("completed")]
        public async Task<IActionResult> GetAllCompletedQuests()
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var completed_quests = await questService.GetAllCompletedQuestsAsync(userId);
                return Ok(completed_quests);
            }
            return Unauthorized();
        }

        [HttpGet("unassigned_pending")]
        public async Task<IActionResult> GetAllUnassignedPendingQuests()
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var unassigned_pending_quests = await questService.GetAllUnassignedPendingQuestsAsync(userId);
                return Ok(unassigned_pending_quests);
            }
            return Unauthorized();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestById(Guid id)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var quest = await questService.GetQuestByIdAsync(id, userId);
                if (quest == null)
                {
                    return NotFound();
                }
                return Ok(quest);
            }
            return Unauthorized();
        }

        /// <summary>
        /// Crée une nouvelle quête pour l'utilisateur authentifié.
        /// </summary>
        /// <param name="questDto">Les données de la quête à créer.</param>
        /// <returns>
        /// Une réponse HTTP 201 Created contenant la quête créée avec son identifiant unique,
        /// ainsi qu'un en-tête Location pointant vers l'endpoint de récupération de la quête.
        /// </returns>
        /// <response code="201">La quête a été créée avec succès. Retourne la quête avec son ID généré.</response>
        /// <response code="400">Les données fournies sont invalides (validation échouée).</response>
        /// <response code="401">L'utilisateur n'est pas authentifié ou le token JWT est invalide.</response>
        /// <response code="409">Une quête est déjà associée à cet hexagone (contrainte d'unicité violée).</response>
        [HttpPost]
        public async Task<IActionResult> CreateQuest([FromBody] QuestCreateDTO questDto)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                try
                {
                    var createdQuest = await questService.CreateQuestAsync(questDto, userId);
                    return CreatedAtAction(nameof(GetQuestById), new { id = createdQuest.Id }, createdQuest);
                }
                catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    return Conflict(new { message = "Une quête est déjà associée à cet hexagone" });
                }
            }
            return Unauthorized();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuest(Guid id, [FromBody] QuestUpdateDTO updatedQuest)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                try
                {
                    var quest = await questService.UpdateQuestAsync(id, updatedQuest, userId);
                    if (quest == null)
                    {
                        return NotFound();
                    }
                    return Ok(quest);
                }
                catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    return Conflict(new { message = "Une quête est déjà associée à cet hexagone" });
                }
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuest(Guid id)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var result = await questService.DeleteQuestAsync(id, userId);
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
