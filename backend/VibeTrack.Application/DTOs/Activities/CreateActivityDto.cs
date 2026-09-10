using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Application.DTOs.Activities
{
    public class CreateActivityDto
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string ActivityType { get; set; }

        [Range(1, 1440)]
        public int TotalTimeMinutes { get; set; }

        [Required]
        public int DailyLogId { get; set; }
    }
}
