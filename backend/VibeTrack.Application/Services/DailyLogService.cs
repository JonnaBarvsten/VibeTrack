using VibeTrack.Application.DTOs.DailyLogs;
using VibeTrack.Application.DTOs.DailyStats;
using VibeTrack.Application.DTOs.MoodLogs;
using VibeTrack.Application.Interfaces;
using VibeTrack.Domain.Entities;
using VibeTrack.Domain.Interfaces;

namespace VibeTrack.Application.Services
{
    public class DailyLogService : IDailyLogService
    {
        private readonly IDailyLogRepository _dailyLogRepository;
        public DailyLogService(IDailyLogRepository dailyLogRepository)
        {
            _dailyLogRepository = dailyLogRepository;
        }

        public async Task<List<DailyLogDto>> GetAllDailyLogsAsync(int userId)
        {
            var userLogs = await _dailyLogRepository.GetLogsByUserIdWithDetailsAsync(userId);

            return userLogs.Select(d => new DailyLogDto
            {
                Id = d.Id,
                Date = d.Date,
                Notes = d.Notes,

                DailyStats = d.DailyStats != null ? new DailyStatDto
                {
                    Id = d.DailyStats.Id,
                    HoursOfSleep = d.DailyStats.HoursOfSleep,
                    DailyLogId = d.DailyStats.DailyLogId,

                    MoodLogs = d.DailyStats.MoodLogs != null
                        ? d.DailyStats.MoodLogs.Select(m => new MoodLogDto
                        {
                            Id = m.Id,
                            LoggedAt = m.LoggedAt,
                            DailyStatsId = m.DailyStatsId,
                            Mood = m.MoodId,
                            MoodName = m.Mood.Name,
                            EnergyLevel = m.EnergyLevelId,
                            StressLevel = m.StressLevelId
                        }).ToList()
                        : new List<MoodLogDto>()
                } : null
            }).ToList();
        }

        public async Task<DailyLogDto?> AddDailyLogAsync(CreateDailyLogDto createDailyLogDto, int userId)
        {
            var newDailyLog = new DailyLog
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Notes = createDailyLogDto.Notes,
                UserId = userId
            };

            await _dailyLogRepository.AddAsync(newDailyLog);

            var dailyLogDto = new DailyLogDto
            {
                Id = newDailyLog.Id,
                Date = newDailyLog.Date,
                Notes = newDailyLog.Notes
            };

            return dailyLogDto;
        }
        public async Task<DailyLogDto?> UpdateDailyLogAsync(int id, UpdateDailyLogDto updateDailyLogDto, int userId)
        {
            var existingDailyLog = await _dailyLogRepository.GetByIdAsync(id);

            if(existingDailyLog != null && existingDailyLog.UserId == userId)
            {
                existingDailyLog.Notes = updateDailyLogDto.Notes;

                await _dailyLogRepository.UpdateAsync(existingDailyLog);

                var dailyLogDto = new DailyLogDto
                {
                    Id = existingDailyLog.Id,
                    Date = existingDailyLog.Date,
                    Notes = existingDailyLog.Notes                
                };

                return dailyLogDto;
            }

            return null; 
        }
        public async Task<bool> DeleteDailyLogAsync(int id, int userId)
        {
            var existingDailyLog = await _dailyLogRepository.GetByIdAsync(id);
            
            
            if(existingDailyLog != null && existingDailyLog.UserId == userId)
            {
                var deletedLog = await _dailyLogRepository.DeleteAsync(id);
                return true;
            }

            return false;
        }

        public async Task<DailyLogDto?> GetDailyLogByIdAsync(int id, int userId)
        {
            var userLogs = await _dailyLogRepository.GetLogsByUserIdWithDetailsAsync(userId);

            return userLogs.Where(d => d.Id == id).Select(d => new DailyLogDto
            {
                Id = d.Id,
                Date = d.Date,
                Notes = d.Notes,
                DailyStats = d.DailyStats != null ? new DailyStatDto
                {
                    Id = d.DailyStats.Id,
                    HoursOfSleep = d.DailyStats.HoursOfSleep,
                    DailyLogId = d.DailyStats.DailyLogId,
                    MoodLogs = d.DailyStats.MoodLogs?.Select(m => new MoodLogDto
                    {
                        Id = m.Id,
                        LoggedAt = m.LoggedAt,
                        DailyStatsId = m.DailyStatsId,
                        Mood = m.MoodId,
                        MoodName = m.Mood.Name,
                        EnergyLevel = m.EnergyLevelId,
                        StressLevel = m.StressLevelId
                    }).ToList() ?? new List<MoodLogDto>()
                } : null
            }).FirstOrDefault();
        }
    }
}
