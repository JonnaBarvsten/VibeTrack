using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Application.DTOs.DailyStats
{
    public class UpdateDailyStatDto
    {
        [Required]
        [Range(0, 24)]
        public decimal HoursOfSleep { get; set; }
    }
}
