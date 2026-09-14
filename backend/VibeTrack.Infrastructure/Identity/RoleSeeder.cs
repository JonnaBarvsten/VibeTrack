using Microsoft.AspNetCore.Identity;
using VibeTrack.Domain.Constans;
using VibeTrack.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace VibeTrack.Infrastructure.Identity
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            var adminRoleExists = await roleManager.RoleExistsAsync(Roles.Admin);

            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(Roles.Admin));
            }

            var userRoleExists = await roleManager.RoleExistsAsync(Roles.User);

            if (!userRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole<int>(Roles.User));
            }

            var adminEmail = "admin@VibeTrack.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if(adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Administrator"
                };

                var createResult = await userManager.CreateAsync(adminUser, "Admin123!");

                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Roles.Admin);
                }
            }

        }
    }
}
