using Microsoft.EntityFrameworkCore;
using OnTheLaneOfHike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Repositories
{
    public class AppRepository : IAppRepository
    {
        DataBaseContext context;
        public AppRepository(DataBaseContext c)
        {
            context = c;
        }
        public IQueryable<PostModel>posts
        {
            get {
                return context.Posts.Include(posts => posts.Member)
                                    .Include(posts => posts.Comments)
                                    .ThenInclude(comment => comment.Member);
                 }
        }



    }
}
