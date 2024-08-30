using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;
using OnlineFood.Models;

namespace OnlineFood.Controllers
{
    // [Authorize]
    public class PaymentController:Controller
    {
        private readonly ApplicationDbContext _context;
        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if(order == null)
            {
                return NotFound();
            }
            var paymentModel = new PaymentModel
            {
                OrderId = orderId,
                Amount = order.TotalAmount
            };
            return View(paymentModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId , OrderId , Amount , PaymentMethod , PaymentDate")] PaymentModel paymentModel)
        {
            if(ModelState.IsValid)
            {
                paymentModel.PaymentDate = DateTime.Now;
                _context.Add(paymentModel);
                var order = await _context.Orders.FindAsync(paymentModel.OrderId);
                if(order != null)
                {
                    order.Status = "Paid";
                    _context.Update(order);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Datails" , "Order" , new{ id = paymentModel.OrderId });
            }
            return View(paymentModel);
        }
        public async Task<IActionResult> Datails (int id)
        {
            var paymentModel = await _context.Payments
            .Include(p => p.Order)
            .FirstOrDefaultAsync(m => m.PaymentId == id);
            if(paymentModel == null)
            {
                return NotFound();
            }
            return View(paymentModel);
        }
        public async Task<IActionResult> Index()
        {
            var payments = await _context.Payments
            .Include(p => p.Order)
            .ToListAsync();
            return View(payments);
        }
        public async Task<IActionResult>Delete (int id )
        {
            var paymentModel = await _context.Payments
            .Include(o=> o.Order)
            .FirstOrDefaultAsync(m => m.PaymentId == id);
            if(paymentModel == null)
            {
                return NotFound();
            }
            return View(paymentModel);
        }
        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentModel = await _context.Payments.FindAsync(id);
            if(paymentModel != null)
            {

                await _context.Payments.FindAsync(id);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
            
        }
        
    }
}