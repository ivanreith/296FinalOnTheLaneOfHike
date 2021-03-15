using Microsoft.AspNetCore.Mvc;
using OnTheLaneOfHike.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using OnTheLaneOfHike.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace OnTheLaneOfHike.Controllers
{
    public class PostController : Controller
    {
          IAppRepository Repo { get; set; }
          DataBaseContext dbcontext { get; set; }
          UserManager<MemberModel> userManager;
         IWebHostEnvironment webHostEnvironment;
        public PostController(IAppRepository r, DataBaseContext context, IWebHostEnvironment hostEnvironment, UserManager<MemberModel> m)
        {
            Repo = r;
            dbcontext = context;
            webHostEnvironment = hostEnvironment;
            userManager = m;
        }


        public IActionResult Index()
        {
            List<PostModel> stories = Repo.posts.ToList<PostModel>();

            return View(stories);
        }
        [Authorize]
        public IActionResult AddPost()
        {
            var post = new PostViewModel();
            // story.Name = User.Identity.Name; field removed from story model
            //post.PostID = 0;
            ViewBag.Action = "Add";
            ViewBag.Users = dbcontext.Members.OrderBy(g => g.Name).ToList();
            return View("AddPost", post);
        }
       [Authorize]
        [HttpPost]
        public IActionResult AddPost(PostViewModel postView)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(postView);
                PostModel post = new PostModel
                {
                    PostTitle = postView.PostTitle,
                    PostTopic = postView.PostTopic,
                    PostText = postView.PostText,
                    Member = userManager.GetUserAsync(User).Result,
                    PostTime = DateTime.Now,
                    ProfilePicture = uniqueFileName,
                };
                Repo.AddPost(post);
                return RedirectToAction("Index", "Post");
            }
            return View();
        }



        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Users = dbcontext.Members.OrderBy(g => g.Name).ToList();
            var post = Repo.GetPostById(id);

            //  var story = Context.Story.Find(id);
            return View(post);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(PostModel post)
        {
            if (ModelState.IsValid)
            {   
                if (post.PostID == 0)
                {
                  
                    post.Member = userManager.GetUserAsync(User).Result;
                    post.Member.Name = post.Member.UserName;
                    post.PostTime = DateTime.Now;
                    Repo.AddPost(post);
                }
                else
                {
                    post.Member = userManager.GetUserAsync(User).Result;
                    post.Member.Name = post.Member.UserName;
                    post.PostTime = DateTime.Now;
                    Repo.UpdatePost(post);
                    return RedirectToAction("Index", "Post");
                }
                return RedirectToAction("Index","Post" );
            }
            else
            {
                  ViewBag.Members = dbcontext.Members.OrderBy(g => g.Name).ToList();
                return View(post);
            }
        }
        private string UploadedFile(PostViewModel post)
        {
            string uniqueFileName = null;

            if (post.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + post.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    post.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {

            var post = dbcontext.Posts.Find(id);
            return View(post);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(PostModel post)
        {
            Repo.DeletePost(post);
            return RedirectToAction("Index", "Post");
        }

        
        [HttpGet]
        public IActionResult AddComment(int id)
        {
            var post = dbcontext.Posts.Find(id); // those 2 lines added to validate id, so no OS command injection on id hidden field
            if (post != null)
            {
                ViewBag.Action = "Comment";
                var commentViewModel = new CommentViewModel { PostID = id };
                return View(commentViewModel);
            }
            return RedirectToAction("Index", "Post");
        }
      
        [HttpPost]
        public RedirectToActionResult AddComment(CommentViewModel commentViewModel)
        {

            // here we pass the data from the view into the new real model for the DB
            var comment = new CommentModel { CommentText = commentViewModel.CommentText };
            if (commentViewModel.PostID > 1000 && commentViewModel.CommentText != null) // to check if it got to this point empty, ZAP testing
            {
                comment.Member = userManager.GetUserAsync(User).Result;
                comment.CommentDate = DateTime.Now;
                // Now we start working on the DB:
                var post = (from p in Repo.posts
                             where p.PostID == commentViewModel.PostID
                             select p).FirstOrDefault<PostModel>();  // after first supposed to go <StoriesModelForm> but visual says it can be omitted.
                                                                            //  now adding the comment to the story object variable that we've retrieved:        
                if (post != null)
                {
                    post.Comments.Add(comment);
                    Repo.UpdatePost(post);
                    return RedirectToAction("Index", "Post");
                }
            }
            else
            {

                return RedirectToAction("Index", "Post");
            }

            return RedirectToAction("Index", "Post");

        }
        /*  NOT SURE IF NEEDED ******************
          public IActionResult Stories()
        {

            List<StoriesModelForm> stories = Repo.stories.ToList<StoriesModelForm>();

            return View(stories);
        }

        SAME THING

          PostModel post = new PostModel
                    {
                        PostTitle = newPost.PostTitle,
                        PostText = newPost.PostText,
                        PostTopic = newPost.PostTopic,
                        PostTime = DateTime.Now,




                    }
         * */
    }
}
