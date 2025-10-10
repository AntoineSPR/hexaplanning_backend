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
    [CheckUser]
    public class StatusController : ControllerBase
    {
        private readonly StatusService statusService;

        public StatusController(StatusService statusService)
        {
            this.statusService = statusService;
        }

        /// <summary>
        /// Get all statuses (non-archived)
        /// </summary>
        /// <returns>List of statuses</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = await statusService.GetAllStatusesAsync();
            return Ok(statuses);
        }

        /// <summary>
        /// Get a specific status by ID
        /// </summary>
        /// <param name="id">Status ID</param>
        /// <returns>Status details</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusById(Guid id)
        {
            var status = await statusService.GetStatusByIdAsync(id);
            if (status == null)
            {
                return NotFound(new { message = "Status not found" });
            }
            return Ok(status);
        }

        /// <summary>
        /// Create a new status
        /// </summary>
        /// <param name="statusDto">Status creation data</param>
        /// <returns>Created status</returns>
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] StatusCreateDTO statusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdStatus = await statusService.CreateStatusAsync(statusDto);
                return CreatedAtAction(nameof(GetStatusById), new { id = createdStatus.Id }, createdStatus);
            }
            catch (DbUpdateException ex)
            {
                return Conflict(new { message = "Error creating status", error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing status
        /// </summary>
        /// <param name="id">Status ID</param>
        /// <param name="updatedStatus">Status update data</param>
        /// <returns>Updated status</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] StatusUpdateDTO updatedStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var status = await statusService.UpdateStatusAsync(id, updatedStatus);
                if (status == null)
                {
                    return NotFound(new { message = "Status not found" });
                }
                return Ok(status);
            }
            catch (DbUpdateException ex)
            {
                return Conflict(new { message = "Error updating status", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a status (archives if used by quests, otherwise physically deletes)
        /// </summary>
        /// <param name="id">Status ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(Guid id)
        {
            var result = await statusService.DeleteStatusAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Status not found" });
            }
            return NoContent();
        }

        /// <summary>
        /// Archive a status
        /// </summary>
        /// <param name="id">Status ID</param>
        /// <returns>No content if successful</returns>
        [HttpPatch("{id}/archive")]
        public async Task<IActionResult> ArchiveStatus(Guid id)
        {
            var result = await statusService.ArchiveStatusAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Status not found" });
            }
            return NoContent();
        }

        /// <summary>
        /// Restore an archived status
        /// </summary>
        /// <param name="id">Status ID</param>
        /// <returns>No content if successful</returns>
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> RestoreStatus(Guid id)
        {
            var result = await statusService.RestoreStatusAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Archived status not found" });
            }
            return NoContent();
        }
    }
}