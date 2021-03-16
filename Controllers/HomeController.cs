using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnTheLaneOfHike.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<MemberModel> userManager;
        public HomeController(ILogger<HomeController> logger, UserManager<MemberModel> userMngr, RoleManager<IdentityRole> roleMngr)
        {
            _logger = logger;
            roleManager = roleMngr;
            userManager = userMngr;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model); 
        }
        public async Task<IActionResult> Admin()
            {
                List<MemberModel> members = new List<MemberModel>();
                foreach (MemberModel member in userManager.Users)
                {
                member.RoleNames = await userManager.GetRolesAsync(member);
                members.Add(member);
                }

                MemberViewModel model = new MemberViewModel
                {
                    Members = members,
                    Roles = roleManager.Roles
                };
                return View(model);
            }
        
        public IActionResult Register()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
 
    
       
    }
}
