using Microsoft.EntityFrameworkCore;
using VibeTrack.Domain.Entities;
using VibeTrack.Domain.Interfaces;

namespace VibeTrack.Infrastructure.Persistence.Repositories
{
    public class DailyLogRepository : IDailyLogRepository
    {
        private readonly VibeTrackDbContext _context;
        public DailyLogRepository(VibeTrackDbContext context)
        {
            _context = context;
        }
        public async Task<List<DailyLog>> GetAllAsync()
        {
            return await _context.DailyLogs.ToListAsync();
        }
        public async Task<DailyLog?> GetByIdAsync(int id)
        {
            return await _context.DailyLogs.FindAsync(id);
        }
        public async Task<DailyLog> AddAsync(DailyLog entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<DailyLog?> DeleteAsync(int id)
        {
            var dailyLog = await GetByIdAsync(id);
            if(dailyLog != null)
            {
                _context.DailyLogs.Remove(dailyLog);
                await _context.SaveChangesAsync();
                return dailyLog;
            }

            return null;
        }
        public async Task<DailyLog> UpdateAsync(DailyLog entity)
        {
            _context.DailyLogs.Update(entity);
            await _context.SaveChangesAsync();
            
            return entity;
        }
    }
}
