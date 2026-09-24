using VibeTrack.Domain.Entities;

namespace VibeTrack.Domain.Interfaces
{
    public interface IMoodLogRepository : IRepository<MoodLog>
    {
        Task<bool> IsDailyStatOwnedByUserAsync(int dailyStatsId, int userId);
        Task<List<Mood>> GetAllMoodsAsync();
    }
}
