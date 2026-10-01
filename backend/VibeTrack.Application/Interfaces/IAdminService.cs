using VibeTrack.Application.DTOs.Admin;

namespace VibeTrack.Application.Interfaces
{
    public interface IAdminService
    {
        Task<List<UserListDto>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
    }
}
