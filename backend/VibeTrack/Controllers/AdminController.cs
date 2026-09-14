using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VibeTrack.Application.Interfaces;

namespace VibeTrack.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController
    {
        private readonly IAuthService _authService;
        public AdminController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpDelete("users/{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var isDeleted = await _authService.DeleteUserAsync(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
