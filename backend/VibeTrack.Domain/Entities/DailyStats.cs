using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VibeTrack.Domain.Entities
{
    public class DailyStats
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(4, 1)")]
        [Range(0,24)]
        public decimal HoursOfSleep { get; set; }

        public DailyLog DailyLog { get; set; }
        public int DailyLogId { get; set; }
        public List<MoodLog> MoodLogs { get; set; }
    }
}
