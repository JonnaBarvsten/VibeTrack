using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Application.DTOs.DailyLogs
{
    public class CreateDailyLogDto
    {
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        [MaxLength(1000)]
        public string? Notes { get; set; }
    }
}
