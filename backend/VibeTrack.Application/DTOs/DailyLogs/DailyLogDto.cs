using VibeTrack.Application.DTOs.DailyStats;

namespace VibeTrack.Application.DTOs.DailyLogs
{
    public class DailyLogDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string? Notes { get; set; }

        public DailyStatDto? DailyStats { get; set; }
    }
}
