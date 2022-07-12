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
            using (var servicesscope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var rolemanager = servicesscope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                if (!await rolemanager.RoleExistsAsync(Role_User.Admin))
                    await rolemanager.CreateAsync(new IdentityRole(Role_User.Admin));
                if (!await rolemanager.RoleExistsAsync(Role_User.User))
                    await rolemanager.CreateAsync(new IdentityRole(Role_User.User));

                // User

                // User - Admin
                var usermanager = servicesscope.ServiceProvider.GetService<UserManager<ApplicationUser >>();

                string AdminUserEmail = "admin@ticket.com";

                var adminUser = await usermanager.FindByEmailAsync(AdminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser ()
                    {
                        FullName = "Admin User",
                        UserName = "admin",
                        Email = AdminUserEmail,
                        EmailConfirmed = true
                    };
                    await usermanager.CreateAsync(newAdminUser, "CASter789@");
                    await usermanager.AddToRoleAsync(newAdminUser, Role_User.Admin);
                }

                // User - User
                string UserEmail = "user@ticket.com";
                var User = await usermanager.FindByEmailAsync(UserEmail);
                if (User == null)
                {
                    var newAppUser = new ApplicationUser ()
                    {
                        FullName = "App User",
                        UserName ="app-user",
                        Email = UserEmail,
                        EmailConfirmed = true
                    };
                    await usermanager.CreateAsync(newAppUser, "CASter789@");
                    await usermanager.AddToRoleAsync(newAppUser, Role_User.User);
                }
            }
        }
    }
}
