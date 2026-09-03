using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Application.DTOs.DailyLogs
{
    public class UpdateDailyLogDto
    {
        [MaxLength(1000)]
        public string? Notes { get; set; }
    }
}
