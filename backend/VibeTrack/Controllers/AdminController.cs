using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VibeTrack.Application.Interfaces;
using VibeTrack.Domain.Constans;

namespace VibeTrack.Api.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : BaseApiController
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpDelete("users/{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var isDeleted = await _adminService.DeleteUserAsync(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
