using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;
using SkipperAgency.Infrastructure.Persistence;
using System;
using System.Threading.Tasks;

namespace SkipperAgency.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<SkipperAgencyDbContext>();

                    if (context.Database.IsSqlServer())
                    {
                        await context.Database.MigrateAsync();
                    }

                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var rolesFromEnum = Enum.GetNames(typeof(RoleEnum));
                    await SkipperAgencyDbContextSeed.SeedDefaultUserAsync(userManager);
                    await SkipperAgencyDbContextSeed.SeedDefaultRolesAsync(roleManager, rolesFromEnum);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
