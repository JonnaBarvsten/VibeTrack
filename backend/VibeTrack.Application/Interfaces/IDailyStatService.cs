using VibeTrack.Application.DTOs.DailyStats;

namespace VibeTrack.Application.Interfaces
{
    public interface IDailyStatService
    {
        Task<List<DailyStatDto>> GetAllDailyStatsAsync();
        Task<DailyStatDto?> GetDailyStatByIdAsync(int id);
        Task<DailyStatDto> AddDailyStatAsync(CreateDailyStatDto createDailyStatDto);
        Task<DailyStatDto?> UpdateDailyStatAsync(int id, UpdateDailyStatDto updateDailyStatDto);
        Task<bool> DeleteDailyStatAsync(int id);
    }
}
