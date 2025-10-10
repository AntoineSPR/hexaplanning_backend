using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Procrastinator.Models;
using Procrastinator.Services;

namespace Procrastinator.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    [CheckUser]
    public class HexAssignmentController : ControllerBase
    {
        private readonly HexAssignmentService hexAssignmentService;
        public HexAssignmentController(HexAssignmentService hexAssignmentService)
        {
            this.hexAssignmentService = hexAssignmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHexAssignments()
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var hexAssignments = await hexAssignmentService.GetAllHexAssignmentsAsync(userId);
                return Ok(hexAssignments);
            }
            return Unauthorized();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHexAssignmentById(Guid id)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var hexAssignment = await hexAssignmentService.GetHexAssignmentByIdAsync(id, userId);
                if (hexAssignment == null)
                {
                    return NotFound();
                }
                return Ok(hexAssignment);
            }
            return Unauthorized();
        }

        [HttpGet("quest/{questId}")]
        public async Task<IActionResult> GetHexAssignmentByQuestId(Guid questId)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var hexAssignment = await hexAssignmentService.GetHexAssignmentByQuestIdAsync(questId, userId);
                if (hexAssignment == null)
                {
                    return NotFound();
                }
                return Ok(hexAssignment);
            }
            return Unauthorized();
        }

        [HttpGet("coordinates/{q}/{r}/{s}")]
        public async Task<IActionResult> GetHexAssignmentByCoordinates(int q, int r, int s)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var hexAssignment = await hexAssignmentService.GetHexAssignmentByCoordinatesAsync(q, r, s, userId);
                if (hexAssignment == null)
                {
                    return NotFound();
                }
                return Ok(hexAssignment);
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> CreateHexAssignment([FromBody] HexAssignmentDTO hexAssignmentDto)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                try
                {
                    var createdHexAssignment = await hexAssignmentService.CreateHexAssignmentAsync(hexAssignmentDto, userId);
                    //return CreatedAtAction(nameof(GetHexAssignmentById), new { id = createdHexAssignment.Id }, createdHexAssignment);
                    return Ok(createdHexAssignment);
                }
                catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    return Conflict(new { message = "Une quête est déjà associée à cet hexagone" });
                }
            }
            return Unauthorized();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHexAssignment(Guid id, [FromBody] HexAssignmentDTO updatedHexAssignment)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                try
                {
                    var result = await hexAssignmentService.UpdateHexAssignmentAsync(id, updatedHexAssignment, userId);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return Ok(result);
                }
                catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    return Conflict(new { message = "Une quête est déjà associée à cet hexagone" });
                }
            }
            return Unauthorized();
        }

        [HttpDelete("coordinates/{q}/{r}/{s}")]
        public async Task<IActionResult> DeleteHexAssignment(int q, int r, int s)
        {
            if (HttpContext.Items["UserId"] is Guid userId)
            {
                var result = await hexAssignmentService.DeleteHexAssignmentAsync(q, r, s, userId);
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
