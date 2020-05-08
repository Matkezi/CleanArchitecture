using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public static class SkipperAgencyDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<AppUser> userManager)
        {
            var defaultUser = new Developer { UserName = "administrator@localhost.com", Email = "administrator@localhost.com" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");
            }
        }

        public static async Task SeedDefaultRolesAsync(RoleManager<IdentityRole> roleManager, string[] roles)
        {
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
