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

        public void AddPost(PostModel post)
        {
            post.PostTime = DateTime.Now;
            // TODO add the actual logged in user example string userName = User.Identity.Name
            context.Posts.Add(post);
            context.SaveChanges();
        }

        public void DeletePost(PostModel post)
        {
            var comments = context.Comments.ToList();
            foreach (CommentModel comment in comments) // added to delete all comments of a story before deleting the story
            {
                foreach (CommentModel storycomment in post.Comments)
                {
                    if (comment.CommentId == storycomment.CommentId)
                    {
                        context.Comments.Remove(comment);
                        context.SaveChanges();
                    }
                }
            }


            context.Posts.Remove(post);
            context.SaveChanges();
        }
        public void UpdatePost(PostModel post)
        {
            context.Posts.Update(post); 
            context.SaveChanges();
        }

        public PostModel GetPostById(int PostId)
        {
            var post = (from p in context.Posts
                         where p.PostID == PostId
                        select p).FirstOrDefault<PostModel>();           
            return post;
        }
        public void DeleteComment(CommentModel comment)
        {
            throw new NotImplementedException();
        }


        public CommentModel GetCommentById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<CommentModel> GetComments()
        {
            throw new NotImplementedException();
        }

        public List<CommentModel> GetCommentsByPost(int Id)
        {
            throw new NotImplementedException();
        }

        

        public void UpdateComment(CommentModel comment)
        {
            throw new NotImplementedException();
        }

      
    }
}
