
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using VibeTrack.Application.Interfaces;
using VibeTrack.Application.Services;
using VibeTrack.Domain.Interfaces;
using VibeTrack.Infrastructure.Persistence;
using VibeTrack.Infrastructure.Persistence.Repositories;

namespace VibeTrack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<VibeTrackDbContext>(options => 
            {
                options.UseSqlServer(builder.Configuration["ConnectionString"]);
            });

            builder.Services.AddScoped<IDailyLogRepository, DailyLogRepository>();
            builder.Services.AddScoped<IDailyLogService, DailyLogService>();

            builder.Services.AddScoped<IDailyStatRepository, DailyStatRepository>();
            builder.Services.AddScoped<IDailyStatService, DailyStatService>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
