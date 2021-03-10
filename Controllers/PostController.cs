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
            return View();
        }
        public IActionResult AddPost()
        {
            return View();
        }
    }
}
