using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;
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
        
        
    }
}