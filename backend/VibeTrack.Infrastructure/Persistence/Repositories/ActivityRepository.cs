using Microsoft.EntityFrameworkCore;
using VibeTrack.Domain.Entities;
using VibeTrack.Domain.Interfaces;

namespace VibeTrack.Infrastructure.Persistence.Repositories
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(VibeTrackDbContext context) : base(context)
        {
            
        }
    }
}
