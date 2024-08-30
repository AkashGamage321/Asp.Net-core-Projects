using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;
using OnlineFood.Models;

namespace OnlineFood.Controllers
{
    // [Authorize]
    public class DeliveryController:Controller
    {
        private readonly ApplicationDbContext _context ;
        public DeliveryController(ApplicationDbContext context)
        {
            _context=  context;
        }
        public async Task<IActionResult>Index()
        {
            var deliveries = await _context.Deliveries
            .Include(o => o.Order)
            .ToListAsync();
            return View(deliveries);

        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var delivery  = await _context.Deliveries
            .Include(d=> d.OrderId)
            .FirstOrDefaultAsync(m => m.DeliveryId== id);
            if(delivery == null)
            {
                return NotFound();
            }
            return View(delivery);
        }
        public async Task<IActionResult>Create(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if(order == null)
            {
                return NotFound();
            }
            var deliveryModel = new DeliveryModel
            {
                OrderId = orderId,
                DeliveryAddress = order.DeliveryAddress,
                DeliveryStatus = "Pending"
            };
            return View(deliveryModel);
        }
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryId , OrderId , DeliveryStatus ,DeliveryDate")] DeliveryModel deliveryModel)
        {
            if(ModelState.IsValid)
            {
                _context.Add(deliveryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var deliveryModel = await _context.Deliveries.FindAsync(id);
            if(deliveryModel == null)
            {
                return NotFound();
            }
            return View(deliveryModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit (int id, [Bind("DeliveryId , OrderId , DeliveryStatus ,DeliveryDate")] DeliveryModel deliveryModel) 
        {
            if(id!= deliveryModel.DeliveryId)
            {
                return NotFound();

            }
            if(ModelState.IsValid)
            {
                try{
                    _context.Update(deliveryModel);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryModel);
        }
        public async Task<IActionResult>Delete(int id)
        {
            var delivery =await _context.Deliveries
            .Include(d => d.Order)
            .FirstOrDefaultAsync();
            if(delivery == null)
            {
                return NotFound();
            }
            return View(delivery);
        }
        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteConfirmed(int id)
        {
            var deliveryModel = await _context.Deliveries.FindAsync(id);
            if(deliveryModel != null)
            {
                _context.Deliveries.Remove(deliveryModel);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }
        
    }
}