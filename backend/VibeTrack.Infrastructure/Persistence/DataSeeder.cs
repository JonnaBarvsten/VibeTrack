using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VibeTrack.Domain.Entities;

namespace VibeTrack.Infrastructure.Persistence.Seeders
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<VibeTrackDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            var testEmail = "testuser@vibetrack.com";
            var testUser = await userManager.FindByEmailAsync(testEmail);

            if (testUser == null)
            {
                testUser = new User 
                { UserName = testEmail,
                    Email = testEmail,
                    EmailConfirmed = true,
                    FirstName = "Test",
                    LastName = "User" 
                };

                await userManager.CreateAsync(testUser, "Test123!");
            }

            if (!await context.DailyLogs.AnyAsync(d => d.UserId == testUser.Id))
            {
                var random = new Random();

                for (int i = 29; i >= 0; i--)
                {
                    var date = DateTime.UtcNow.AddDays(-i);
                    var dayOfWeek = date.DayOfWeek;

                    int moodId;
                    int energyId;
                    int stressId;
                    decimal sleepHours;
                    string activityName;

                    if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
                    {
                        moodId = 1;         
                        energyId = 4;       
                        stressId = 1;       
                        sleepHours = 8.5m;
                        activityName = "Promenad";
                    }
                    else
                    {
                        moodId = (i % 2 == 0) ? 3 : 6;  
                        energyId = 3;                  
                        stressId = 2;                  
                        sleepHours = 7.0m;
                        activityName = "Träning";
                    }

                    var dailyLog = new DailyLog
                    {
                        UserId = testUser.Id,
                        Date = DateOnly.FromDateTime(date),
                        Notes = $"Testlogg för {date.ToString("yyyy-MM-dd")}",
                        DailyStats = new DailyStat
                        {
                            HoursOfSleep = sleepHours,
                            MoodLogs = new List<MoodLog>
                            {
                                new MoodLog
                                {
                                    LoggedAt = date,
                                    MoodId = moodId,
                                    EnergyLevelId = energyId,
                                    StressLevelId = stressId
                                }
                            }
                        },

                        Activities = new List<Activity>
                        {
                            new Activity
                            {
                                ActivityType = activityName,
                                TotalTimeMinutes = random.Next(30, 60)
                            }
                        }
                    };

                    context.DailyLogs.Add(dailyLog);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}