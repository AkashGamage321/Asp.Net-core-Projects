using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;
using BookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Controllers;

public class TimeSlotController : Controller
{
    private readonly ApplicationDbContext _context; 
    public TimeSlotController (ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult>Index (int serviceId)
    {
        var timeSlots = await _context.TimeSlots
        .Where(t => t.ServiceID == serviceId)
        .ToListAsync();
        if(timeSlots == null)
        {
            return NotFound();
        }
        return View(timeSlots);
    }
    public IActionResult Create(int id)
    {
        var timeSlot = new TimeSlotModel
        {
            ServiceID= id
        };
        return View(timeSlot);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TimeSlotModel timeSlot)
    {
        if(ModelState.IsValid)
        {
            _context.Add(timeSlot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index) ,new{serviceId = timeSlot.ServiceID} );
            
        }
        return View(timeSlot);
    }
    public async Task<IActionResult>Edit (int id )
    {
        var timeSlot = await _context.TimeSlots.FindAsync(id);
        if(timeSlot == null)
        {
            return NotFound();
        }
        return View(timeSlot);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Edit(int id ,TimeSlotModel timeSlotModel)
    {
        if(id != timeSlotModel.TimeSlotId)
        {
            return NotFound();
        }
        if(ModelState.IsValid)
        {
            _context.Update(timeSlotModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index) , new{serviceId = timeSlotModel.ServiceID});
        }
        return View(timeSlotModel);
    }
    public async Task<IActionResult> Delete (int id)
    {
        var timeSlot = await _context.TimeSlots
        .FirstOrDefaultAsync(m => m.TimeSlotId == id);
        if(timeSlot == null)
        {
            return NotFound();
        }
        return View(timeSlot);
    }
    [HttpPost , ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var timeSlot = await _context.TimeSlots.FindAsync(id);
        _context.TimeSlots.Remove(timeSlot);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index) , new{ serviceId = timeSlot.ServiceID});
        
    }
}