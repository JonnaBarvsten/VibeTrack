using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Application.DTOs.MoodLogDto
{
    public class CreateMoodLogDto
    {
        [Range(0, 5)]
        public int EnergyLevel { get; set; }

        [Range(0, 5)]
        public int Mood { get; set; }

        [Range(0, 5)]
        public int StressLevel { get; set; }
    }
}
