using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Domain.Entities
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ActivityType { get; set; }
        public int TotalTimeMinutes { get; set; }

        public int DailyLogId { get; set; }
        public DailyLog DailyLog { get; set; }
    }
}
