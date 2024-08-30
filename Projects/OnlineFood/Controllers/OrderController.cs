using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineFood.Data;
using OnlineFood.Models;

namespace OnlineFood.Controllers
{
    // [Authorize]
    public class OrderController :Controller
    {
        private readonly ApplicationDbContext _context ; 

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
            .Include(o=> o.Restaurant)
            .Include(o=> o.MenuItem)
            .ToListAsync();
            return View(orders);
        }
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
            .Include(o => o.Restaurant)
            .Include(o => o.MenuItem)
            .FirstOrDefaultAsync(m => m.OrderId == id);
            if(order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants , "RestaurantId", "Name");
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems , "MenuItemId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create([Bind("OrderId,UserId,RestaurantId,OrderDate,TotalAmount,DeliveryAddress,PhoneNumber,MenuItems")] OrderModel orderModel)
        {    
                if(ModelState.IsValid)
            {
                _context.Add(orderModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
                ViewData["RestaurantId"] = new SelectList(_context.Restaurants , "RestaurantId", "Name",orderModel.RestaurantId);
                ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "MenuItemId", "Name", orderModel.MenuItemId);

                return View(orderModel);
        }    
        public async Task<IActionResult> Edit(int id)
        {
            var orderModel = await _context.Orders.FindAsync(id);
            if(orderModel == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants ,"RestaurantId", "Name", orderModel.RestaurantId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "MenuItemId", "Name", orderModel.MenuItemId);
            return View(orderModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id , [Bind("OrderId,UserId,RestaurantId,OrderDate,TotalAmount,DeliveryAddress,PhoneNumber,MenuItems")] OrderModel orderModel)
        {
            if(id != orderModel.OrderId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _context.Update(orderModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "Name", orderModel.RestaurantId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "MenuItemId", "Name", orderModel.MenuItemId);
            return View(orderModel);
        }
        public async Task<IActionResult> Delete(int id )
        {
            var order = await _context.Orders
            .Include(o=> o.Restaurant)
            .Include(o=> o.MenuItem)
            .FirstOrDefaultAsync(m => m.OrderId == id);
            if(order== null)
            {
                return NotFound();
            }
            return View(order);
        }
        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        

    }
}