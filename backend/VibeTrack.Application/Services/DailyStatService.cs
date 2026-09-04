using VibeTrack.Application.DTOs.DailyStats;
using VibeTrack.Application.Interfaces;
using VibeTrack.Domain.Entities;
using VibeTrack.Domain.Interfaces;

namespace VibeTrack.Application.Services
{
    public class DailyStatService : IDailyStatService
    {
        private readonly IDailyStatRepository _dailyStatRepository;
        public DailyStatService(IDailyStatRepository dailyStatRepository)
        {
            _dailyStatRepository = dailyStatRepository;
        }
        public async Task<DailyStatDto> AddDailyStatAsync(CreateDailyStatDto createDailyStatDto)
        {
            var newDailyStat = new DailyStat
            {
                HoursOfSleep = createDailyStatDto.HoursOfSleep
            };

            await _dailyStatRepository.AddAsync(newDailyStat);


            var dailyStatDto = new DailyStatDto
            {
                Id = newDailyStat.Id,
                HoursOfSleep = newDailyStat.HoursOfSleep
            };

            return dailyStatDto;
        }

        public async Task<bool> DeleteDailyStatAsync(int id)
        {
            var deletedEntity = await _dailyStatRepository.DeleteAsync(id);

            return deletedEntity != null;
        }

        public async Task<List<DailyStatDto>> GetAllDailyStatsAsync()
        {
            var dailyStats = await _dailyStatRepository.GetAllAsync();

            var newDailyStatsDto = dailyStats.Select(d => new DailyStatDto 
            {
                Id = d.Id,
                HoursOfSleep = d.HoursOfSleep
            }).ToList();

            return newDailyStatsDto;
        }

        public async Task<DailyStatDto?> GetDailyStatByIdAsync(int id)
        {
            var dailyStat = await _dailyStatRepository.GetByIdAsync(id);

            if (dailyStat != null)
            {
                var dailyStatDto = new DailyStatDto
                {
                    Id = dailyStat.Id,
                    HoursOfSleep = dailyStat.HoursOfSleep
                };

                return dailyStatDto;
            }

            return null; 
        }

        public async Task<DailyStatDto?> UpdateDailyStatAsync(int id, UpdateDailyStatDto updateDailyStatDto)
        {
            var existingDailyStat = await _dailyStatRepository.GetByIdAsync(id);

            if(existingDailyStat != null)
            {
                existingDailyStat.HoursOfSleep = updateDailyStatDto.HoursOfSleep;

                await _dailyStatRepository.UpdateAsync(existingDailyStat);

                var dailyStatDto = new DailyStatDto
                {
                    Id = existingDailyStat.Id,
                    HoursOfSleep = existingDailyStat.HoursOfSleep
                };

                return dailyStatDto;
            }

            return null;
        }
    }
}
