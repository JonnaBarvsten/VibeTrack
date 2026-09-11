using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VibeTrack.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public abstract class BaseApiController : ControllerBase
    {
        protected int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
