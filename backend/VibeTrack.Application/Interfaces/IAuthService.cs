using VibeTrack.Application.DTOs.Auth;

namespace VibeTrack.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
