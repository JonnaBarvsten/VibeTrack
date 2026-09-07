using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Application.DTOs.DailyLogs
{
    public class CreateDailyLogDto
    {
        [MaxLength(1000)]
        public string? Notes { get; set; }
    }
}
