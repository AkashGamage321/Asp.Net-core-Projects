using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController :Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Users ()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
        public async Task<IActionResult>Threads()
        {
            var threads = await _context.Threads.ToListAsync();
            return View(threads);
        }
        // public async Task<IActionResult> EditUser (string id)
        // {
        //     var user = await _context.Users.FindAsync(id);
        //     if(user == null)
        //     {
        //         return NotFound();
        //     }
        //     var model = new EditUserViewModel
        //     {
        //         UserId = user.Id,
        //         Username = user.UserName,
        //         Email = user.Email,
        //         Roles = _context.Admins.Where(ur => ur.UserId == user.Id).ToList()
        //     return View(model);
        // }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> EditUser(EditUserViewModel model)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         var user = await _context.Users.FindAsync(model.UserId);
        //         if(user == null)
        //         {
        //             return NotFound();
        //         }
        //         user.UserName = model.Username;
        //         user.Email = model.Email;

        //         _context.Users.Update(user);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(model);
        // }
        public IActionResult DeleteThread(int id)
        {
            var thread = _context.Threads.Find(id);
            if(thread == null)
            {
                return NotFound();
            }
            return View(thread);
        }
        [HttpPost , ActionName("DeleteThread")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteThreadConfirmed(int id)
        {
            var thread = await _context.Threads.FindAsync(id);
            _context.Threads.Remove(thread);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Threads));
        }

        
    }
}