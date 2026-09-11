using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using VibeTrack.Application.Interfaces;
using VibeTrack.Application.Services;
using VibeTrack.Domain.Entities;
using VibeTrack.Domain.Interfaces;
using VibeTrack.Infrastructure.Identity;
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


            builder.Services.AddIdentityCore<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<VibeTrackDbContext>();

            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
                ?? throw new InvalidOperationException("Jwt configuration is missing");

            builder.Services.AddSingleton(jwtSettings);
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            builder.Services.AddScoped<IDailyLogRepository, DailyLogRepository>();
            builder.Services.AddScoped<IDailyLogService, DailyLogService>();

            builder.Services.AddScoped<IDailyStatRepository, DailyStatRepository>();
            builder.Services.AddScoped<IDailyStatService, DailyStatService>();

            builder.Services.AddScoped<IMoodLogRepository, MoodLogRepository>();
            builder.Services.AddScoped<IMoodLogService, MoodLogService>();

            builder.Services.AddScoped<IActivityService, ActivityService>();
            builder.Services.AddScoped<IActivityRepository, ActivityRepository>();

            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    //ÄNDRA SEN TILL RÄTT LOCALHOST
                    policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Cookies.TryGetValue("Jwt", out var token))
                            {
                                context.Token = token;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddAuthorization();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
