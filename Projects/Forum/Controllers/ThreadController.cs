using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Models;
using Forum.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
    [Authorize]
    public class ThreadController: Controller
    {
        private readonly ApplicationDbContext _context ;
        private readonly UserManager<UserModel> _userManager;
        public ThreadController(ApplicationDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager ;
        }
        [AllowAnonymous]
        public IActionResult Index(int categoryId)
        {
            var threads = _context.Threads
            .Where(t=> t.CategoryId == categoryId)
            .Select(t => new ThreadListViewModel 
            {
                ThreadId = t.ThreadId , 
                Title = t.Title , 
                Author = t.User.UserName,
                NumberOfReplies = t.Posts.Count -1,
                LastReplayDate = t.Posts.Max(p => p.CreatedDate)
            }).ToList();
            return View(threads);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details (int id)
        {
            var thread = await _context.Threads
            .Where(t=> t.ThreadId == id)
            .Select(t => new ThreadDetailViewModel
            {
                ThreadId = t.ThreadId , 
                Title = t.Title , 
                Content = t.Posts.FirstOrDefault().Content ,
                Author = t.User.UserName , 
                CreatedDate = t.CreateDate, 
                Upvotes = t.UpvoteCount,
                Replies = t.Posts.Skip(1).Select(p => new ReplyViewModel{
                    ReplayId = p.PostId , 
                    Content = p.Content , 
                    Author = p.User.UserName , 
                    CreatedDate = p.CreatedDate
                }).ToList()
            }).FirstOrDefaultAsync();//(t => t.ThreadId == id);
            if(thread == null)
            {
                return NotFound();
            }
            return View(thread);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create (PostViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var thread = new ThreadModel
                {
                    Title = model.Title,
                    CategoryId = model.categoryId , 
                    UserId = user.UserId , 
                    CreateDate = DateTime.Now
                    
                };
                var post = new PostModel
                {
                    Thread = thread,
                    Content = model.Content , 
                    UserId = user.UserId , 
                    CreatedDate = DateTime.Now
                };
                _context.Threads.Add(thread);
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details) , new { id = thread.ThreadId});
            }
            return View(model);

        }
        public async Task<IActionResult>Edit (int id)
        {
            var thread = await _context.Threads.FindAsync(id);
            if(thread == null)
            {
                return NotFound();
            }
            var model = new PostViewModel
            {
                ThreadId = thread.ThreadId ,
                Title = thread.Title ,
                Content = thread.Posts.FirstOrDefault()?.Content
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id , PostViewModel model)
        {
            if(id != model.ThreadId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                var thread = await _context.Threads.FindAsync(id);
                if(thread ==null)
                {
                    return NotFound();
                }
                thread.Title = model.Title;
                thread.Posts.FirstOrDefault().Content = model.Content;
                 
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details) , new {id = thread.ThreadId});

            }
            return View(model);
        }
        public async Task<IActionResult> Detele(int id)
        {
            var thread  = await _context.Threads.FindAsync(id);
            if(thread == null)
            {
                return NotFound();
            }
            return View(thread);
        } 
        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thread = await _context.Threads.FindAsync(id);
            if(thread == null)
            {
                return NotFound();
            }
            _context.Threads.Remove(thread);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}