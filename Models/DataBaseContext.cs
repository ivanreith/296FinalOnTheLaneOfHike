using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace OnTheLaneOfHike.Models
{
    public class DataBaseContext : IdentityDbContext<MemberModel>
    {
        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        { }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<MemberModel> Members { get; set; } //REmoved due to Identity inheritance, parent class would do it
        public DbSet<EventModel> Event { get; set; }
        public DbSet<ProposalModel> Proposals { get; set; }

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
                MemberModel user = new MemberModel { UserName = username, Name = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }

            }

        }



    }
}
