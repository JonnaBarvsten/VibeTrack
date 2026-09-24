using VibeTrack.Application.DTOs.MoodLogs;

namespace VibeTrack.Application.DTOs.DailyStats
{
    public class DailyStatDto
    {
        public int Id { get; set; }
        public decimal HoursOfSleep { get; set; }
        public int DailyLogId { get; set; }

        public List<MoodLogDto> MoodLogs { get; set; }

    }
}