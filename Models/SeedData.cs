using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Models
{
    public class SeedData
    {
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<MemberModel> userManager = serviceProvider.GetRequiredService<UserManager<MemberModel>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string username = "admin";
            string password = "Sesame@1";
            string roleName = "Admin";
            // Creating the role the first time
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // creating the username the first time and adding it to the role
            if (await userManager.FindByNameAsync(username) == null)
            {
                MemberModel user = new MemberModel { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }

            }

        }




    }
}
