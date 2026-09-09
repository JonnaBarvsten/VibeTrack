namespace VibeTrack.Domain.Entities
{
    public class MoodLog
    {
        public int Id { get; set; }
        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

        public EnergyLevel EnergyLevel { get; set; }
        public int EnergyLevelId { get; set; }

        public Mood Mood { get; set; }
        public int MoodId { get; set; }

        public StressLevel StressLevel { get; set; }
        public int StressLevelId { get; set; }

        public DailyStat DailyStats { get; set; }
        public int DailyStatsId { get; set; }

    }
}
