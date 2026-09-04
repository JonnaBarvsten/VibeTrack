using VibeTrack.Domain.Entities;
using VibeTrack.Domain.Interfaces;

namespace VibeTrack.Infrastructure.Persistence.Repositories
{
    public class DailyStatRepository : Repository<DailyStat>, IDailyStatRepository
    {
        public DailyStatRepository(VibeTrackDbContext context) : base(context)
        {
            
        }
    }
}
