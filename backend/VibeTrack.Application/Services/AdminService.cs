using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VibeTrack.Application.DTOs.Admin;
using VibeTrack.Application.Interfaces;
using VibeTrack.Domain.Entities;

namespace VibeTrack.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        public AdminService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UserListDto>> GetAllUsersAsync()
        {
            var userListDto = await _userManager.Users.Select(u => new UserListDto
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                CreatedAt = u.CreatedAt
            }).ToListAsync();

            return userListDto;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }
    }
}
