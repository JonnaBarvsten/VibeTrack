using VibeTrack.Application.DTOs.DailyStats;
using VibeTrack.Application.Interfaces;
using VibeTrack.Domain.Entities;
using VibeTrack.Domain.Interfaces;

namespace VibeTrack.Application.Services
{
    public class DailyStatService : IDailyStatService
    {
        private readonly IDailyStatRepository _dailyStatRepository;
        private readonly IDailyLogRepository _dailyLogRepository;
        public DailyStatService(IDailyStatRepository dailyStatRepository, IDailyLogRepository dailyLogRepository)
        {
            _dailyStatRepository = dailyStatRepository;
            _dailyLogRepository = dailyLogRepository;
        }
        public async Task<DailyStatDto?> AddDailyStatAsync(CreateDailyStatDto createDailyStatDto, int userId)
        {
            var dailyLog = await _dailyLogRepository.GetByIdAsync(createDailyStatDto.DailyLogId);
            if (dailyLog != null && dailyLog.UserId == userId)
            {
                var newDailyStat = new DailyStat
                {
                    HoursOfSleep = createDailyStatDto.HoursOfSleep,
                     DailyLogId = createDailyStatDto.DailyLogId
                };

                await _dailyStatRepository.AddAsync(newDailyStat);


                var dailyStatDto = new DailyStatDto
                {
                    Id = newDailyStat.Id,
                    HoursOfSleep = newDailyStat.HoursOfSleep,
                    DailyLogId = newDailyStat.DailyLogId
                };

                return dailyStatDto;
            }

            return null;
        }

        public async Task<bool> DeleteDailyStatAsync(int id, int userId)
        {
            var dailyStat = await _dailyStatRepository.GetByIdAsync(id);

            if (dailyStat == null)
            {
                return false;
            }

            var dailyLog = await _dailyLogRepository.GetByIdAsync(dailyStat.DailyLogId);

            if (dailyLog != null && dailyLog.UserId == userId) 
            {
                var deletedEntity = await _dailyStatRepository.DeleteAsync(id);
                return true;
            }

            return false;
        }

        public async Task<List<DailyStatDto>> GetAllDailyStatsAsync(int userId)
        {
            var userLog = await _dailyLogRepository.GetAllAsync();
            var userLogIds = userLog.Where(dl => dl.UserId == userId)
                .Select(dl => dl.Id)
                .ToList();

            var dailyStats = await _dailyStatRepository.GetAllAsync();
            var userStats = dailyStats.Where(ds => userLogIds.Contains(ds.DailyLogId));

            var newDailyStatsDto = userStats.Select(d => new DailyStatDto
            {
                Id = d.Id,
                HoursOfSleep = d.HoursOfSleep,
                DailyLogId = d.DailyLogId
            }).ToList();

            return newDailyStatsDto;
        }

        public async Task<DailyStatDto?> GetDailyStatByIdAsync(int id, int userId)
        {
            var dailyStat = await _dailyStatRepository.GetByIdAsync(id);

            if (dailyStat == null)
            {
                return null;
            }

            var userLog = await _dailyLogRepository.GetByIdAsync(dailyStat.DailyLogId);

            if (userLog != null && userLog.UserId == userId)
            {
                var dailyStatDto = new DailyStatDto
                {
                    Id = dailyStat.Id,
                    HoursOfSleep = dailyStat.HoursOfSleep,
                    DailyLogId = dailyStat.DailyLogId
                };

                return dailyStatDto;
            }

            return null;
        }

        public async Task<DailyStatDto?> UpdateDailyStatAsync(int id, UpdateDailyStatDto updateDailyStatDto, int userId)
        {
            var existingDailyStat = await _dailyStatRepository.GetByIdAsync(id);

            if(existingDailyStat == null)
            {
                return null;
            }

            var userLog = await _dailyLogRepository.GetByIdAsync(existingDailyStat.DailyLogId);

            if (userLog != null && userLog.UserId == userId)
            {
                existingDailyStat.HoursOfSleep = updateDailyStatDto.HoursOfSleep;

                await _dailyStatRepository.UpdateAsync(existingDailyStat);

                var dailyStatDto = new DailyStatDto
                {
                    Id = existingDailyStat.Id,
                    HoursOfSleep = existingDailyStat.HoursOfSleep,
                    DailyLogId = existingDailyStat.DailyLogId
                };

                return dailyStatDto;
            }

            return null;
        }
    }
}
