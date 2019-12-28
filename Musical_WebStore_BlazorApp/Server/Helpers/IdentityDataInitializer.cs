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
            if (userManager.FindByNameAsync("test").GetAwaiter().GetResult() == null)
            {
                var user = new User
                {
                    Id = "0",
                    UserName = "test",
                    Email = "test@localhost", 
                    EmailConfirmed = true,
                    Position = "Manager"
                };

                IdentityResult result = userManager.CreateAsync(user, "123abcG$").GetAwaiter().GetResult();


                if (!result.Succeeded)
                {
                    var completeException = string.Join(", ", result.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding users: " + completeException);
                }
                var result1 = userManager.AddToRoleAsync(user, "CompanyAdmin").GetAwaiter().GetResult();

                if (!result1.Succeeded)
                {
                    var completeException = string.Join(", ", result1.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding users: " + completeException);
                }
            }
            if (userManager.FindByNameAsync("test_user").GetAwaiter().GetResult() == null)
            {
                var user = new User
                {
                    Id = "1",
                    UserName = "test_user",
                    Email = "test_user@localhost", 
                    EmailConfirmed = true,
                    Position = "Manager"
                };

                IdentityResult result = userManager.CreateAsync(user, "123abcG$").GetAwaiter().GetResult();


                if (!result.Succeeded)
                {
                    var completeException = string.Join(", ", result.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding users: " + completeException);
                }
                var result1 = userManager.AddToRoleAsync(user, "CompanyOperator").GetAwaiter().GetResult();

                if (!result1.Succeeded)
                {
                    var completeException = string.Join(", ", result1.Errors.Select(i => i.Description));

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

                var result1 = userManager.AddToRoleAsync(user, "SuperAdmin").GetAwaiter().GetResult();

                if (!result1.Succeeded)
                {
                    var completeException = string.Join(", ", result1.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding users: " + completeException);
                }
            }
            if (userManager.FindByNameAsync("service_user").GetAwaiter().GetResult() == null)
            {
                var user = new User
                {
                    Id = "service",
                    UserName = "service_user",
                    Email = "service_user@localhost", 
                    EmailConfirmed = true,
                    Position = "Service Manager"
                };

                IdentityResult result = userManager.CreateAsync(user, "123abcG$").GetAwaiter().GetResult();


                if (!result.Succeeded)
                {
                    var completeException = string.Join(", ", result.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding users: " + completeException);
                }

                var result1 = userManager.AddToRoleAsync(user, "ServiceOperator").GetAwaiter().GetResult();

                if (!result1.Succeeded)
                {
                    var completeException = string.Join(", ", result1.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding users: " + completeException);
                }
            }
            if (userManager.FindByNameAsync("worker_user").GetAwaiter().GetResult() == null)
            {
                var user = new User
                {
                    Id = "worker",
                    UserName = "worker_user",
                    Email = "worker_user@localhost", 
                    EmailConfirmed = true,
                    Position = "Higher Worker"
                };

                IdentityResult result = userManager.CreateAsync(user, "123abcG$").GetAwaiter().GetResult();


                if (!result.Succeeded)
                {
                    var completeException = string.Join(", ", result.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding users: " + completeException);
                }

                var result1 = userManager.AddToRoleAsync(user, "Worker").GetAwaiter().GetResult();

                if (!result1.Succeeded)
                {
                    var completeException = string.Join(", ", result1.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding users: " + completeException);
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("SuperAdmin").GetAwaiter().GetResult())
            {
                var role = new IdentityRole
                {
                    Name = "SuperAdmin",
                };

                IdentityResult result = roleManager.CreateAsync(role).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    var completeException = string.Join(", ", result.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding roles: " + completeException);
                }
            }

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

            if (!roleManager.RoleExistsAsync("ServiceAdmin").GetAwaiter().GetResult())
            {
                var role = new IdentityRole
                {
                    Name = "ServiceAdmin",
                };

                IdentityResult result = roleManager.CreateAsync(role).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    var completeException = string.Join(", ", result.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding roles: " + completeException);
                }
            }

            if (!roleManager.RoleExistsAsync("CompanyAdmin").GetAwaiter().GetResult())
            {
                var role = new IdentityRole
                {
                    Name = "CompanyAdmin",
                };

                IdentityResult result = roleManager.CreateAsync(role).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    var completeException = string.Join(", ", result.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding roles: " + completeException);
                }
            }
            if (!roleManager.RoleExistsAsync("ServiceOperator").GetAwaiter().GetResult())
            {
                var role = new IdentityRole
                {
                    Name = "ServiceOperator",
                };

                IdentityResult result = roleManager.CreateAsync(role).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    var completeException = string.Join(", ", result.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding roles: " + completeException);
                }
            }
            if (!roleManager.RoleExistsAsync("CompanyOperator").GetAwaiter().GetResult())
            {
                var role = new IdentityRole
                {
                    Name = "CompanyOperator",
                };

                IdentityResult result = roleManager.CreateAsync(role).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    var completeException = string.Join(", ", result.Errors.Select(i => i.Description));

                    throw new InvalidOperationException("Exception at seeding roles: " + completeException);
                }
            }
            if (!roleManager.RoleExistsAsync("Worker").GetAwaiter().GetResult())
            {
                var role = new IdentityRole
                {
                    Name = "Worker",
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
