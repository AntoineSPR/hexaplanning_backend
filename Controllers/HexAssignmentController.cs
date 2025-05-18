using Microsoft.AspNetCore.Mvc;
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
            var createdHexAssignment = await hexAssignmentService.CreateHexAssignmentAsync(hexAssignmentDto);
            return CreatedAtAction(nameof(GetHexAssignmentById), new { id = createdHexAssignment.Id }, createdHexAssignment);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHexAssignment(int id, [FromBody] HexAssignmentDTO updatedHexAssignment)
        {
            var result = await hexAssignmentService.UpdateHexAssignmentAsync(id, updatedHexAssignment);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHexAssignment(int id)
        {
            var result = await hexAssignmentService.DeleteHexAssignmentAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
