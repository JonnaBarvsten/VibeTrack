using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Domain.Entities
{
    public class DailyLog
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        
        [MaxLength(1000)]
        public string? Notes { get; set; }

        public int DailyStatsId { get; set; }
        public DailyStat DailyStats { get; set; }
        public List<Activity> Activities { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

    }
}
