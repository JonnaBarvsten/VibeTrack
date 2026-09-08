using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Application.DTOs.DailyStats
{
    public class CreateDailyStatDto
    {
        [Required]
        [Range(0, 24)]
        public decimal HoursOfSleep { get; set; }

        [Required]
        public int DailyLogId { get; set; }
    }
}
