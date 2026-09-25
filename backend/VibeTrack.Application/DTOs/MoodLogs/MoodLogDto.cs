namespace VibeTrack.Application.DTOs.MoodLogs
{
    public class MoodLogDto
    {
        public int Id { get; set; }
        public DateTime LoggedAt { get; set; }
        public int DailyStatsId { get; set; }
        public int EnergyLevel { get; set; }
        public int Mood { get; set; }
        public string? MoodName { get; set; }
        public int StressLevel { get; set; }
    }
}
