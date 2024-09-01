using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Models;
using Forum.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class AccountController:Controller
    {
        public UserManager<UserModel> _userManager ;
        public SignInManager<UserModel> _signInManager ;
        public AccountController(UserManager<UserModel> userManager , SignInManager<UserModel> signInManager)
        {
            _userManager = userManager; 
            _signInManager = signInManager;

        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if(ModelState.IsValid)
            {
                var user = new UserModel{ UserName = model.Username , Email = model.Email };

                var result = await _userManager.CreateAsync(user , model.Password);
                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent:false);
                    return RedirectToAction("Index" , "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty , error.Description);
                }
            }
            return View(model);
        }
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"]=  returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Login(LoginViewModel model , string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username , model.Password , model.RememberMe , lockoutOnFailure:false);
                if(result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError(string.Empty , "Invalid login Attempt! ");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index" , "Home");
        }
        public IActionResult AccessDanied()
        {
            return View();
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if(Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else{
                return RedirectToAction(nameof(HomeController.Index) , "Home");
            }
        }
        public IActionResult Profile(ProfileViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}
