using VibeTrack.Domain.Entities;

namespace VibeTrack.Domain.Interfaces
{
    public interface IDailyLogRepository : IRepository<DailyLog>
    {
        Task<List<DailyLog>> GetLogsByUserIdWithDetailsAsync(int userId);
    }
}
