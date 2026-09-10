using VibeTrack.Application.DTOs.Activities;

namespace VibeTrack.Application.Interfaces
{
    public interface IActivityService
    {
        Task<List<ActivityDto>> GetAllActivitiesAsync(int userId);
        Task<ActivityDto?> GetActivityByIdAsync(int id, int userId);
        Task<ActivityDto?> AddActivityAsync(CreateActivityDto createActivityDto, int userId);
        Task<ActivityDto?> UpdateActivityAsync(int id, UpdateActivityDto updateActivityDto, int userId);
        Task<bool> DeleteActivityAsync(int id, int userId); 
    }
}
