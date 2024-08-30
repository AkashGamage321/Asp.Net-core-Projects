// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore.Internal;
// using OnlineFood.Data;
// using OnlineFood.Models;
// using OnlineFood.ViewModels;

// namespace OnlineFood.Controllers
// {
//     public class AccountController:Controller
//     {
//         private UserManager<ApplicationDbContext> _userManager ;
//         private SignInManager<ApplicationDbContext> _signInManager;

//         public AccountController(UserManager<ApplicationDbContext> userManager , SignInManager<ApplicationDbContext> signInManager)
//         {
//             _userManager = userManager;
//             _signInManager = signInManager;
//         }
//         [HttpGet]
//         public IActionResult Login(string returnUrl =null)
//         {
//             ViewData["ReturnUrl"] = returnUrl;
//             return View();
//         }
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult>Login(LoginViewModel model , string returnUrl =null)
//         {
//             ViewData["RestaurantUrl"] = returnUrl;
//             if(ModelState.IsValid)
//             {
//                 var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
//                 if(result.Succeeded)
//                 {
//                     return RedirectToAction(returnUrl);
//                 }
//                 ModelState.AddModelError(string.Empty ,"Invalid Login Request");

//             }
//             return View(model);
//         }
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult>LogOut()
//         {
//             await _signInManager.SignOutAsync();
//             return RedirectToAction("Index" ,"Home");
//         }

//         [HttpGet]
//         public IActionResult AccessDenied()
//         {
//             return View();
//         }
//         private IActionResult RedirectToLocal (string returnUrl)
//         {
//             if(Url.IsLocalUrl(returnUrl))
//             {
//                 return Redirect(returnUrl);
//             }
//             else{
//                 return RedirectToAction("Index", "Home");
//             }
//         }

//         [HttpGet]
//         public IActionResult Register()
//         {
//             return View();
//         }
//         // [HttpPost]
//         // [ValidateAntiForgeryToken]
//         // public async Task<IActionResult> Register(RegisterViewModel model)
//         // {
//         //     var user = new UserModel { UserName= model.Email , Email = model.Email};
//         //     var result = await _userManager.CreateAsync(user , model.Password);

//         // }

//     [HttpPost]
//     [ValidateAntiForgeryToken]
//     public async Task<IActionResult> Register(RegisterViewModel model)
//     {
//         if (ModelState.IsValid)
//         {
//             var user = new UserModel { UserName = model.Email, Email = model.Email };
//             var result = await _userManager.CreateAsync(user, model.Password);
//             if (result.Succeeded)
//             {
//                 await _signInManager.SignInAsync(user, isPersistent: false);
//                 return RedirectToAction("Index", "Home");
//             }
//             foreach (var error in result.Errors)
//             {
//                 ModelState.AddModelError(string.Empty, error.Description);
//             }
//         }
//         return View(model);
//     }


//     }
// }