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
    public class DataBaseContext : IdentityDbContext
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
    }
}
