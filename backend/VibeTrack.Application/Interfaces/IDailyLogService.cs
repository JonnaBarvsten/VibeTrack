using VibeTrack.Application.DTOs.DailyLogs;

namespace VibeTrack.Application.Interfaces
{
    public interface IDailyLogService
    {
        Task<List<DailyLogDto>> GetAllDailyLogsAsync(int userId);
        Task<DailyLogDto?> GetDailyLogByIdAsync(int id, int userId);
        Task<DailyLogDto?> AddDailyLogAsync(CreateDailyLogDto createDailyLogDto, int userId);
        Task<DailyLogDto?> UpdateDailyLogAsync(int id, UpdateDailyLogDto updateDailyLogDto, int userId);
        Task<bool> DeleteDailyLogAsync(int id, int userId);
    }
}
