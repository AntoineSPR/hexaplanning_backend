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
    public class HexAssignmentController: ControllerBase
    {
        private readonly HexAssignmentService hexAssignmentService;
        public HexAssignmentController(HexAssignmentService hexAssignmentService)
        {
            this.hexAssignmentService = hexAssignmentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHexAssignments()
        {
            var hexAssignments = await hexAssignmentService.GetAllHexAssignmentsAsync();
            return Ok(hexAssignments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHexAssignmentById(int id)
        {
            var hexAssignment = await hexAssignmentService.GetHexAssignmentByIdAsync(id);
            if (hexAssignment == null)
            {
                return NotFound();
            }
            return Ok(hexAssignment);
        }

        [HttpGet("coordinates/{q}/{r}/{s}")]
        public async Task<IActionResult> GetHexAssignmentByCoordinates(int q, int r, int s)
        {
            var hexAssignment = await hexAssignmentService.GetHexAssignmentByCoordinatesAsync(q, r, s);
            if (hexAssignment == null)
            {
                return NotFound();
            }
            return Ok(hexAssignment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHexAssignment([FromBody] HexAssignmentDTO hexAssignmentDto)
        {
            try { 
            var createdHexAssignment = await hexAssignmentService.CreateHexAssignmentAsync(hexAssignmentDto);
            return CreatedAtAction(nameof(GetHexAssignmentById), new { id = createdHexAssignment.Id }, createdHexAssignment);
            } catch (DbUpdateException ex) when(ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                return Conflict(new { message = "Une quête est déjà associée à cet hexagone" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHexAssignment(int id, [FromBody] HexAssignmentDTO updatedHexAssignment)
        {
            try
            {
                var result = await hexAssignmentService.UpdateHexAssignmentAsync(id, updatedHexAssignment);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            } catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                return Conflict(new { message = "Une quête est déjà associée à cet hexagone" });
            }
        }

        [HttpDelete("coordinates/{q}/{r}/{s}")]
        public async Task<IActionResult> DeleteHexAssignment(int q, int r, int s)
        {
            var result = await hexAssignmentService.DeleteHexAssignmentAsync(q, r, s);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
