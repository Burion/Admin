using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Musical_WebStore_BlazorApp.Server.Data.Models;

namespace Musical_WebStore_BlazorApp.Server.Helpers
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("test_user").GetAwaiter().GetResult() == null)
            {
                var user = new User
                {
                    UserName = "test_user",
                    Email = "test_user@localhost", 
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "123abcG$").GetAwaiter().GetResult();


                if (!result.Succeeded)
                {
                    var completeException = string.Join(", ", result.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding users: " + completeException);
                }

            }

            if (userManager.FindByNameAsync("test_admin").GetAwaiter().GetResult() == null)
            {
                var user = new User
                {
                    UserName = "test_admin",
                    Email = "test_admin@localhost",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "123abcG$").GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    var completeException = string.Join(", ", result.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding users: " + completeException);
                }

                var result1 = userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();

                if (!result1.Succeeded)
                {
                    var completeException = string.Join(", ", result1.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding users: " + completeException);
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                var role = new IdentityRole
                {
                    Name = "Admin",
                };

                IdentityResult result = roleManager.CreateAsync(role).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    var completeException = string.Join(", ", result.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding roles: " + completeException);
                }
            }
        }
    }
}
