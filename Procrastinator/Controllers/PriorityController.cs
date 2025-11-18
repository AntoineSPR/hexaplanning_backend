using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Procrastinator.Models;
using Procrastinator.Services;
using Procrastinator.Utilities;

namespace Procrastinator.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        private readonly PriorityService priorityService;

        public PriorityController(PriorityService priorityService)
        {
            this.priorityService = priorityService;
        }

        /// <summary>
        /// Get all priorities (non-archived)
        /// </summary>
        /// <returns>List of priorities</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllPriorities()
        {
            var priorities = await priorityService.GetAllPrioritiesAsync();
            return Ok(priorities);
        }

        /// <summary>
        /// Get a specific priority by ID
        /// </summary>
        /// <param name="id">Priority ID</param>
        /// <returns>Priority details</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPriorityById(Guid id)
        {
            var priority = await priorityService.GetPriorityByIdAsync(id);
            if (priority == null)
            {
                return NotFound(new { message = "Priority not found" });
            }
            return Ok(priority);
        }

        /// <summary>
        /// Create a new priority
        /// </summary>
        /// <param name="priorityDto">Priority creation data</param>
        /// <returns>Created priority</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePriority([FromBody] PriorityCreateDTO priorityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdPriority = await priorityService.CreatePriorityAsync(priorityDto);
                return CreatedAtAction(nameof(GetPriorityById), new { id = createdPriority.Id }, createdPriority);
            }
            catch (DbUpdateException ex)
            {
                return Conflict(new { message = "Error creating priority", error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing priority
        /// </summary>
        /// <param name="id">Priority ID</param>
        /// <param name="updatedPriority">Priority update data</param>
        /// <returns>Updated priority</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePriority(Guid id, [FromBody] PriorityUpdateDTO updatedPriority)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var priority = await priorityService.UpdatePriorityAsync(id, updatedPriority);
                if (priority == null)
                {
                    return NotFound(new { message = "Priority not found" });
                }
                return Ok(priority);
            }
            catch (DbUpdateException ex)
            {
                return Conflict(new { message = "Error updating priority", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a priority (archives if used by quests, otherwise physically deletes)
        /// </summary>
        /// <param name="id">Priority ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriority(Guid id)
        {
            var result = await priorityService.DeletePriorityAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Priority not found" });
            }
            return NoContent();
        }

        /// <summary>
        /// Archive a priority
        /// </summary>
        /// <param name="id">Priority ID</param>
        /// <returns>No content if successful</returns>
        [HttpPatch("{id}/archive")]
        public async Task<IActionResult> ArchivePriority(Guid id)
        {
            var result = await priorityService.ArchivePriorityAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Priority not found" });
            }
            return NoContent();
        }

        /// <summary>
        /// Restore an archived priority
        /// </summary>
        /// <param name="id">Priority ID</param>
        /// <returns>No content if successful</returns>
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> RestorePriority(Guid id)
        {
            var result = await priorityService.RestorePriorityAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Archived priority not found" });
            }
            return NoContent();
        }
    }
}