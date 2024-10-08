using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    
    public class AccountController:Controller
    {
        private readonly UserManager<IdentityUser> _userManager ;
        private readonly SignInManager<IdentityUser>_signInManager;

        public AccountController(UserManager<IdentityUser> userManager , SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager  = signInManager;
        }
        [HttpGet]
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
                var user = new IdentityUser{ UserName = model.Username ,Email = model.Email };
                var  result = await _userManager.CreateAsync(user , model.Password);
                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user ,isPersistent:false);
                    return RedirectToAction("Index" , "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("" , error.Description);
                }

            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index" , "Home");
                }
                ModelState.AddModelError("" , "Invalid Login attempt. ");

            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index" , "Home");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        
    }
}