using VibeTrack.Application.DTOs.DailyLogs;
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

        public async Task<List<DailyLogDto>> GetAllDailyLogsAsync()
        {
            var dailyLogs = await _dailyLogRepository.GetAllAsync();

            var dailyLogsDto = dailyLogs.Select(d => new DailyLogDto 
            {
                Id = d.Id,
                Date = d.Date,
                Notes = d.Notes
            }).ToList();

            return dailyLogsDto;
        }
        public async Task<DailyLogDto?> GetDailyLogByIdAsync(int id)
        {
            var dailyLog = await _dailyLogRepository.GetByIdAsync(id);

            if(dailyLog != null)
            {
                var dailyLogDto = new DailyLogDto
                {
                    Id = dailyLog.Id,
                    Date = dailyLog.Date,
                    Notes= dailyLog.Notes
                };

                return dailyLogDto;
            }

            return null;
        }
        public async Task<DailyLogDto> AddDailyLogAsync(CreateDailyLogDto createDailyLogDto)
        {
            var newDailyLog = new DailyLog
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Notes = createDailyLogDto.Notes
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
        public async Task<DailyLogDto?> UpdateDailyLogAsync(int id, UpdateDailyLogDto updateDailyLogDto)
        {
            var existingDailyLog = await _dailyLogRepository.GetByIdAsync(id);

            if(existingDailyLog != null)
            {
                existingDailyLog.Notes = updateDailyLogDto.Notes;

                await _dailyLogRepository.UpdateAsync(existingDailyLog);

                var DailyLogDto = new DailyLogDto
                {
                    Id = existingDailyLog.Id,
                    Date = existingDailyLog.Date,
                    Notes = existingDailyLog.Notes                };

                return DailyLogDto;
            }

            return null; 
        }
        public async Task<bool> DeleteDailyLogAsync(int id)
        {
            var deletedLog = await _dailyLogRepository.DeleteAsync(id);

            return deletedLog != null;
        }
    }
}
