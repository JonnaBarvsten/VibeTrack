using VibeTrack.Application.DTOs.DailyStats;

namespace VibeTrack.Application.Interfaces
{
    public interface IDailyStatService
    {
        Task<List<DailyStatDto>> GetAllDailyStatsAsync(int userId);
        Task<DailyStatDto?> GetDailyStatByIdAsync(int id, int userId);
        Task<DailyStatDto?> AddDailyStatAsync(CreateDailyStatDto createDailyStatDto, int userId);
        Task<DailyStatDto?> UpdateDailyStatAsync(int id, UpdateDailyStatDto updateDailyStatDto, int userId);
        Task<bool> DeleteDailyStatAsync(int id, int userId);
    }
}
