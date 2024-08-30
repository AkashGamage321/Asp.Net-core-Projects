using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Controllers
{
    [Authorize]
    public class ServiceController:Controller
    {
        public readonly ApplicationDbContext _context; 
        public ServiceController(ApplicationDbContext context)
        {
            _context= context;
        }
        public async Task<IActionResult> Index()
        {
            var services = await _context.Services.ToListAsync();
            return View(services);
        }
        public async Task<IActionResult>Details(int id)
        {
            var services = await _context.Services
            .FirstOrDefaultAsync(m => m.ServiceId == id);
            if(services == null)
            {
                return NotFound();
            }
            return View(services);
        }
        public async  Task<IActionResult> Create([Bind("ServiceId,ServiceName,Description,Price")] ServiceModel service)
        {
            if(ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }
        public async Task<IActionResult>Edit (int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var service = await _context.Services.FindAsync(id);
            if(service == null)
            {
                return NotFound();
            }
            return View(service);
        }
        public async Task<IActionResult>Edit (int id, [Bind("ServiceId,ServiceName,Description,Price")] ServiceModel service)
        {
            if(id != service.ServiceId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _context.Update(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        public async Task<IActionResult>Delete (int id )
        {
            var service = await _context.Services
            .FirstOrDefaultAsync(m => m.ServiceId == id);
            if(service== null)
            {
                return NotFound();
            }
            return View(service);
        }
        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 
    }
}