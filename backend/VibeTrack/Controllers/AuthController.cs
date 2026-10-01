using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VibeTrack.Api.Extensions;
using VibeTrack.Application.DTOs.Auth;
using VibeTrack.Application.Interfaces;

namespace VibeTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            if (!string.IsNullOrEmpty(result.Token))
            {
                Response.AppendJwtCookie(result.Token);
            }
                return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var result = await _authService.LoginAsync(login);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            if (!string.IsNullOrEmpty(result.Token))
            {
                Response.AppendJwtCookie(result.Token);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.DeleteJwtCookie();
            return Ok(new { message = "Utloggad" });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var roles = User.FindAll(ClaimTypes.Role)
                .Select(claim => claim.Value)
                .ToList();

            return Ok(new
            {
                username = User.Identity?.Name,
                roles
            });
        }
    }
}