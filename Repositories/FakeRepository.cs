using OnTheLaneOfHike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Repositories
{
    public class FakeRepository : IAppRepository
    {
        List<PostModel> Posts = new List<PostModel>();
        public IQueryable<PostModel> posts { get { return Posts.AsQueryable(); } }
        public void AddPost(PostModel post)
        {
            post.PostID = Posts.Count();
            Posts.Add(post);
        }

        public void DeleteComment(CommentModel comment)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(PostModel post)
        {
            Posts.Remove(post);
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

        public PostModel GetPostById(int PostId)
        {
            foreach (PostModel post in Posts)
            {
                if (post.PostID == PostId)
                {
                    return post;
                }

            }
            return null;
        }

        public void UpdateComment(CommentModel comment)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(PostModel post)
        {
            Posts[0] = post; 
        }
    }
}
