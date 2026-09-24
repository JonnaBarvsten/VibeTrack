using VibeTrack.Application.DTOs.MoodLogs;
using VibeTrack.Domain.Entities;
using VibeTrack.Domain.Interfaces;

namespace VibeTrack.Application.Interfaces
{
    public interface IMoodLogService
    {
        Task<List<MoodLogDto>> GetAllMoodLogsAsync(int userId);
        Task<MoodLogDto?> GetMoodLogByIdAsync(int id, int userId);
        Task<MoodLogDto?> AddMoodLogAsync(CreateMoodLogDto createMoodLogDto, int userId);
        Task<MoodLogDto?> UpdateMoodLogAsync(int id, UpdateMoodLogDto updateMoodLogDto, int userId);
        Task<bool> DeleteMoodLogAsync(int id, int userId);
        Task<List<Mood>> GetAllMoodsAsync();
    }
}
