using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Application.DTOs.MoodLogDto
{
    public class UpdateMoodLogDto
    {
        [Required]
        public int DailyStatsId { get; set; }

        [Range(0, 5)]
        public int EnergyLevel { get; set; }

        [Range(0, 5)]
        public int Mood { get; set; }

        [Range(0, 5)]
        public int StressLevel { get; set; }
    }
}
