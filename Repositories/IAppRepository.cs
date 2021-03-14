using OnTheLaneOfHike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Repositories
{
    public interface IAppRepository
    {
        IQueryable<PostModel> posts { get; }
        void AddPost(PostModel post);  // create
        PostModel GetPostById(int PostId); //Retrieve a story by topic
        void UpdatePost(PostModel post);
        void DeletePost(PostModel post);
        List<CommentModel> GetCommentsByPost(int Id);
        List<CommentModel> GetComments();
        CommentModel GetCommentById(int Id);
        void DeleteComment(CommentModel comment);
        void UpdateComment(CommentModel comment);

    }
}
