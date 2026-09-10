using VibeTrack.Application.DTOs.Activities;
using VibeTrack.Application.Interfaces;
using VibeTrack.Domain.Entities;
using VibeTrack.Domain.Interfaces;

namespace VibeTrack.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IDailyLogRepository _dailyLogRepository;

        public ActivityService(IActivityRepository activityRepository, IDailyLogRepository dailyLogRepository)
        {
            _activityRepository = activityRepository;
            _dailyLogRepository = dailyLogRepository;
        }

        public async Task<ActivityDto?> AddActivityAsync(CreateActivityDto createActivityDto, int userId)
        {
            var dailyLog = await _dailyLogRepository.GetByIdAsync(createActivityDto.DailyLogId);

            if(dailyLog == null || dailyLog.UserId != userId)
            {
                return null;
            }

            var newActivity = new Activity
            {
                ActivityType = createActivityDto.ActivityType,
                TotalTimeMinutes = createActivityDto.TotalTimeMinutes,
                DailyLogId = createActivityDto.DailyLogId
            };

            await _activityRepository.AddAsync(newActivity);

            var activityDto = new ActivityDto
            {
                Id = newActivity.Id,
                ActivityType = newActivity.ActivityType,
                TotalTimeMinutes = newActivity.TotalTimeMinutes,
                DailyLogId= newActivity.DailyLogId
            };

            return activityDto;
        }

        public async Task<bool> DeleteActivityAsync(int id, int userId)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            
            if(activity == null)
            {
                return false;
            }

            var dailyLog = await _dailyLogRepository.GetByIdAsync(activity.DailyLogId);

            if(dailyLog == null || dailyLog.UserId != userId)
            {
                return false;
            }

            await _activityRepository.DeleteAsync(id);

            return true; 
        }

        public async Task<ActivityDto?> GetActivityByIdAsync(int id, int userId)
        {
            var activityLog = await _activityRepository.GetByIdAsync(id);

            if(activityLog == null)
            {
                return null;
            }

            var dailyLog = await _dailyLogRepository.GetByIdAsync(activityLog.DailyLogId);

            if(dailyLog == null || dailyLog.UserId != userId)
            {
                return null;
            }

            var activityDto = new ActivityDto
            {
                Id = activityLog.Id,
                ActivityType = activityLog.ActivityType,
                TotalTimeMinutes = activityLog.TotalTimeMinutes,
                DailyLogId = activityLog.DailyLogId
            };

            return activityDto;
        }

        public async Task<List<ActivityDto>> GetAllActivitiesAsync(int userId)
        {
            var userLogs = await _dailyLogRepository.GetAllAsync();

            var userLogsIds = userLogs.Where(dl => dl.UserId == userId)
                .Select(dl => dl.Id)
                .ToList();

            var activityLog = await _activityRepository.GetAllAsync();
            var userActivity = activityLog.Where(al => userLogsIds.Contains(al.DailyLogId));

            var activityDto = userActivity.Select(al => new ActivityDto
            {
                Id = al.Id,
                ActivityType = al.ActivityType,
                TotalTimeMinutes= al.TotalTimeMinutes,
                DailyLogId= al.DailyLogId
            }).ToList();

            return activityDto;
        }

        public async Task<ActivityDto?> UpdateActivityAsync(int id, UpdateActivityDto updateActivityDto, int userId)
        {
            var activity = await _activityRepository.GetByIdAsync(id);

            if(activity == null)
            {
                return null; 
            }

            var userLog = await _dailyLogRepository.GetByIdAsync(activity.DailyLogId);

            if(userLog == null || userLog.UserId != userId)
            {
                return null; 
            }

            activity.ActivityType = updateActivityDto.ActivityType;
            activity.TotalTimeMinutes = updateActivityDto.TotalTimeMinutes;

            await _activityRepository.UpdateAsync(activity);

            var activityDto = new ActivityDto
            {
                Id = activity.Id,
                ActivityType = activity.ActivityType,
                TotalTimeMinutes = activity.TotalTimeMinutes,
                DailyLogId = activity.DailyLogId
            };

            return activityDto;
        }
    }
}
