using Microsoft.EntityFrameworkCore;
using VibeTrack.Domain.Entities;
using VibeTrack.Domain.Interfaces;

namespace VibeTrack.Infrastructure.Persistence.Repositories
{
    public class MoodLogRepository : Repository<MoodLog>, IMoodLogRepository
    {
        public MoodLogRepository(VibeTrackDbContext context) : base(context)
        {
            
        }

        public override async Task<List<MoodLog>> GetAllAsync()
        {
            return await _context.MoodLogs
                .Include(ml => ml.DailyStats)
                .ThenInclude(ds => ds.DailyLog)
                .ToListAsync();
        }

        public async Task<bool> IsDailyStatOwnedByUserAsync(int dailyStatsId, int userId)
        {
            var isValid = await _context.DailyStats.AnyAsync(ds => ds.Id == dailyStatsId && ds.DailyLog.UserId == userId);

            return isValid;
        }

        public async Task<List<Mood>> GetAllMoodsAsync()
        {
            return await _context.Moods.ToListAsync();
        }
    }
}
