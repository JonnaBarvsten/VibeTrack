using VibeTrack.Application.DTOs.DailyLogs;

namespace VibeTrack.Application.Interfaces
{
    public interface IDailyLogService
    {
        Task<List<DailyLogDto>> GetAllDailyLogsAsync();
        Task<DailyLogDto?> GetDailyLogByIdAsync(int id);
        Task<DailyLogDto> AddDailyLogAsync(CreateDailyLogDto createDailyLogDto);
        Task<DailyLogDto?> UpdateDailyLogAsync(int id, UpdateDailyLogDto updateDailyLogDto);
        Task<bool> DeleteDailyLogAsync(int id);
    }
}
