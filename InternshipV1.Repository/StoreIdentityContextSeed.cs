using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedRolesAndSuperAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "SuperAdmin","Admin", "User" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var superAdminUser = new IdentityUser
            {
                UserName = "emadhamdy",
                Email = "emad@gmail.com",
            };

            if (userManager.Users.All(u => u.UserName != superAdminUser.UserName))
            {
                var result = await userManager.CreateAsync(superAdminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                }
            }
        }
    }
}
