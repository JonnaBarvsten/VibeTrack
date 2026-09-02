using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VibeTrack.Domain.Entities;

namespace VibeTrack.Infrastructure.Persistence
{
    public class VibeTrackDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public VibeTrackDbContext(DbContextOptions<VibeTrackDbContext> options) : base(options)
        {

        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<DailyLog> DailyLogs { get; set; }
        public DbSet<DailyStats> DailyStats { get; set; }
        public DbSet<EnergyLevel> EnergyLevels { get; set; }
        public DbSet<Mood> Moods { get; set; }
        public DbSet<MoodLog> MoodLogs { get; set; }
        public DbSet<StressLevel> StressLevels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DailyLog>()
                .HasOne(dl => dl.DailyStats)
                .WithOne(ds => ds.DailyLog)
                .HasForeignKey<DailyStats>(ds => ds.DailyLogId);

            //Dataseeding for EnergyLevel
            modelBuilder.Entity<EnergyLevel>()
               .HasData(
                    new EnergyLevel { Id = 1, Level = 1, Name = "Helt Slut!" },
                    new EnergyLevel { Id = 2, Level = 2, Name = "Låg Energi" },
                    new EnergyLevel { Id = 3, Level = 3, Name = "Neutral" },
                    new EnergyLevel { Id = 4, Level = 4, Name = "Pigg" },
                    new EnergyLevel { Id = 5, Level = 5, Name = "DUNDER!" }
                );

            modelBuilder.Entity<Mood>().HasData(
                // Posivity
                new Mood { Id = 1, Name = "Glad" },
                new Mood { Id = 2, Name = "Överlycklig" },
                new Mood { Id = 3, Name = "Motiverad" },
                new Mood { Id = 4, Name = "Tacksam" },
                new Mood { Id = 5, Name = "Stolt" },

                // Calm 
                new Mood { Id = 6, Name = "Lugn" },
                new Mood { Id = 7, Name = "Harmonisk" },
                new Mood { Id = 8, Name = "Lättad" },

                // Low 
                new Mood { Id = 9, Name = "Trött" },
                new Mood { Id = 10, Name = "Ledsen" },
                new Mood { Id = 11, Name = "Nere" },
                new Mood { Id = 12, Name = "Apatisk" },

                // Stressed
                new Mood { Id = 13, Name = "Överväldigad" },
                new Mood { Id = 14, Name = "Ångestfylld" },
                new Mood { Id = 15, Name = "Frustrerad" },
                new Mood { Id = 16, Name = "Arg" }
            );

            modelBuilder.Entity<StressLevel>()
                .HasData(
                    new StressLevel { Id = 1, Level = 1, Name = "Ingen Stress" },
                    new StressLevel { Id = 2, Level = 2, Name = "Mild Stress" },
                    new StressLevel { Id = 3, Level = 3, Name = "Måttlig Stress" },
                    new StressLevel { Id = 4, Level = 4, Name = "Hög Stress" },
                    new StressLevel { Id = 5, Level = 5, Name = "Kritisk Stress!" }
                );
        }
    }
}
