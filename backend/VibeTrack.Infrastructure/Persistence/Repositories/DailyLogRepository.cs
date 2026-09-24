using Microsoft.EntityFrameworkCore;
using VibeTrack.Domain.Entities;
using VibeTrack.Domain.Interfaces;

namespace VibeTrack.Infrastructure.Persistence.Repositories
{
    public class DailyLogRepository : Repository<DailyLog>, IDailyLogRepository
    {
        public DailyLogRepository(VibeTrackDbContext context) : base(context)
        {
            
        }

        public async Task<List<DailyLog>> GetLogsByUserIdWithDetailsAsync(int userId)
        {
            return await _context.DailyLogs
                .Include(l => l.DailyStats)
                .ThenInclude(ds => ds.MoodLogs)
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.Date)
                .ToListAsync();
        }
    }
}
