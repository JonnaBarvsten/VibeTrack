using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Application.DTOs.MoodLogs
{
    public class UpdateMoodLogDto
    {
        [Required]
        public int DailyStatsId { get; set; }

        [Range(1, 5)]
        public int EnergyLevel { get; set; }

        [Range(1, 16)]
        public int Mood { get; set; }

        [Range(1, 5)]
        public int StressLevel { get; set; }
    }
}
