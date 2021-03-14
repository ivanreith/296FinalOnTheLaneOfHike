using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnTheLaneOfHike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Controllers
{
   
        public class AccountController : Controller
        {
            private UserManager<MemberModel> userManager;
            private SignInManager<MemberModel> signInManager;

            public AccountController(UserManager<MemberModel> usermngr, SignInManager<MemberModel> signInMngr)
            {
                userManager = usermngr;
                signInManager = signInMngr;
            }
            // Register login and logout methods go after here : ++++++++++++++
            public IActionResult Register()
            {

                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Register(RegisterViewModel model)
            {
                if (ModelState.IsValid)
                {
                MemberModel user = new MemberModel { UserName = model.UserName, Name = model.UserName };
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                return View(model);
            }
            [HttpGet]
            public IActionResult LogIn(string returnURL = "")
            {
                var model = new LoginViewModel { ReturnUrl = returnURL };
                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> LogIn(LoginViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var result = await signInManager.PasswordSignInAsync(
                        model.Username, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");

                        }

                    }
                }
                ModelState.AddModelError("", "Invalid combination user/password. Try again :P");
                return View(model);
            }


            public async Task<IActionResult> LogOut()
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            public IActionResult Index()
            {
                return View();
            }
            public ViewResult AccessDenied()
            {
                return View();
            }
        }
    }
