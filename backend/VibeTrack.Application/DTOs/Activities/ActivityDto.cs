using System.ComponentModel.DataAnnotations;

namespace VibeTrack.Application.DTOs.Activities
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string ActivityType { get; set; }
        public int TotalTimeMinutes { get; set; }
    }
}
