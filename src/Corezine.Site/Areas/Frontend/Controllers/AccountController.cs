using Corezine.Domain.Data;
using Corezine.Domain.Models;
using Corezine.Site.Areas.Frontend.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corezine.Site.Areas.Frontend.Controllers
{
    [Area("Frontend")]
    public class AccountController : Controller
    {
        public AccountController(
            SignInManager<AppUser> loginManager, 
            UserManager<AppUser> userManager, 
            RoleManager<AppRole> rolesManager,
            CorezineDatabaseContext context)
        {
            LoginManager = loginManager;
            UserManager = userManager;
            RolesManager = rolesManager;
            Context = context;
        }

        public SignInManager<AppUser> LoginManager { get; }
        public UserManager<AppUser> UserManager { get; }
        public RoleManager<AppRole> RolesManager { get; }
        public CorezineDatabaseContext Context { get; }

        [HttpGet]
        public IActionResult SignIn(String ReturnUrl)
        {
            if (User.Identity.IsAuthenticated) { return Redirect("~/"); }
            return View(new SignInModel() {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            signInModel.ReturnUrl = signInModel.ReturnUrl ?? Url.Content("~/");
            if(ModelState.IsValid)
            {
                var result = await LoginManager.PasswordSignInAsync(signInModel.Username, signInModel.Password, signInModel.RememberMe, false);
                if(result.Succeeded) { return RedirectToPage(signInModel.ReturnUrl); }
                else
                {
                    ModelState.AddModelError("Authentication", "Invalid Username or Password");
                    return View();
                }
            }
            else { return View(signInModel); }
            
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) { return Redirect("~/"); }
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser() { Email = registerModel.Email, UserName = registerModel.Username };
                Boolean IsFirstUser = (Context.Users.Count() == 0);
                var result = await this.UserManager.CreateAsync(user, registerModel.Password);
                if(result.Succeeded)
                {
                    if(IsFirstUser)
                    {
                        //Add Administrator Role and Assign to First User
                        AppRole adminRole = new AppRole() { Name = "Administrator" };
                        var roleResult = await this.RolesManager.CreateAsync(adminRole);
                        if(roleResult.Succeeded)
                        {
                            await this.UserManager.AddToRoleAsync(user, adminRole.Name);
                        }
                    }
                    await this.LoginManager.SignInAsync(user, true);
                    return Redirect("~/");
                }
                else
                {
                    foreach(var error in result.Errors) { ModelState.AddModelError(string.Empty, error.Description); }
                }
            }
            return View(registerModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(String returnUrl)
        {
            await this.LoginManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}
