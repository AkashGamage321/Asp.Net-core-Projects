using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Controllers
{
    [Authorize]
    public class BookingController:Controller
    {
        private readonly ApplicationDbContext _context ; 
        public BookingController (ApplicationDbContext context)
        {
            _context= context ;
        }
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
            .Include(u => u.User)
            .Include(s=> s.Service)
            .Include(t => t.TimeSlot)
            .ToListAsync();
            return View(bookings);
        }

        public async Task<IActionResult> Details(int? id )
        {
            if(id == null)
            {
                return NotFound();
            }
            var booking = await _context.Bookings
            .Include(u => u.User)
            .Include(t => t.TimeSlot)
            .Include(s => s.Service)
            .FirstOrDefaultAsync(b=> b.BookingId == id);
            if(booking == null )
            {
                return NotFound();
            }
            return View(booking);
        }
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.Services , "ServiceId", "ServiceName");
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlots , "TimeSlotId",  "StartTime");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create([Bind("BookingId , ServiceId , TimeSlotId , UserId , BookingDate , Status ")] BookingModel bookingModel)
        {
            if(ModelState.IsValid)
            {
                _context.Add(bookingModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"]= new SelectList(_context.Services , "ServiceId" , "ServiceName" , bookingModel.ServiceId);
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlots , "TimeSlotId" , "StartTime" , bookingModel.TimeSlotId);
            return View(bookingModel); 
        } 
        public async Task<IActionResult> Edit(int id )
        {
            var booking = await _context.Bookings.FindAsync(id);
            if(booking== null)
            {
                return NotFound();
            }
            ViewData["ServiceId"]= new SelectList(_context.Services , "ServiceId" , "ServiceName" , booking.ServiceId);
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlots , "TimeSlotId" , "StartTime" , booking.TimeSlotId);
            return View(booking);
        }
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId , ServiceId , TimeSlotId , UserId , BookingDate , Status ")] BookingModel booking)
        {
            if(id != booking.BookingId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"]= new SelectList(_context.Services , "ServiceId" , "ServiceName" , booking.ServiceId);
            ViewData["TimeSlotId"] = new SelectList(_context.TimeSlots , "TimeSlotId" , "StartTime" , booking.TimeSlotId);
            return View(booking);
        }
        public async Task<IActionResult>Delete(int id )
        {
            var booking = await _context.Bookings
            .Include(s => s.Service)
            .Include(t => t.TimeSlot)
            .Include(u => u.User)
            .FirstOrDefaultAsync(b => b.BookingId == id);
            if(booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }
        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteConfirmed(int id )
        {
            var booking = await _context.Bookings.FindAsync(id);
            if(booking == null)
            {
                return NotFound();
            }
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        
    }
}