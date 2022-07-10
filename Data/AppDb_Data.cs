using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_movie.Data.Static;
using web_movie.Models;

namespace web_movie.Data
{
    public class AppDb_Data
    {
        public static async Task Seed_User_Role(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var rolemanager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                if (!await rolemanager.RoleExistsAsync(Role_User.Admin))
                    await rolemanager.CreateAsync(new IdentityRole(Role_User.Admin));
                if (!await rolemanager.RoleExistsAsync(Role_User.User))
                    await rolemanager.CreateAsync(new IdentityRole(Role_User.User));

                // User

                // User - Admin
                var usermanager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
                string AdminUserEmail = "admin@ticket.com";
                var adminUser = await usermanager.FindByEmailAsync(AdminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin",
                        Email = AdminUserEmail,
                        EmailConfirmed = true
                    };
                    await usermanager.CreateAsync(newAdminUser,"123");
                    await usermanager.AddToRoleAsync(newAdminUser, Role_User.Admin);
                }

                // User - User
                string UserEmail = "user@ticket.com";
                var User = await usermanager.FindByEmailAsync(UserEmail);
                if (User == null)
                {
                    var newAppUser = new AppUser()
                    {
                        FullName = "App User",
                        UserName ="app-user",
                        Email = UserEmail,
                        EmailConfirmed = true
                    };
                    await usermanager.CreateAsync(newAppUser, "321");
                    await usermanager.AddToRoleAsync(newAppUser, Role_User.User);
                }
            }
        }
    }
}
