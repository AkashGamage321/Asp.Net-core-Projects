using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Data;
using BookingSystem.Models;
using BookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Authorize]
    public class PaymentController:Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public async Task<IActionResult>Index()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return View(payments);
        }
        public async Task<IActionResult>Details(int id)
        {
            var payment =   await _paymentService.GetPaymentByIdAsync(id);
            if(payment == null)
            {
                return NotFound();

            }
            return View(payment);
        }
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentModel paymentModel)
        {
            if(ModelState.IsValid)
            {
                await _paymentService.ProcessPaymentAsync(paymentModel);
                return RedirectToAction(nameof(Index));
            }
            return View(paymentModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit (int id , PaymentModel paymentModel )
        {
            if (id != paymentModel.PaymentId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try{

                    _paymentService.UpdatePaymentAsync(paymentModel);
                }
                catch(Exception)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(paymentModel);
        }

        public async Task<IActionResult>Delete(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if(payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }
        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteConfirmed(int id)
        {
            await _paymentService.DeletePaymentAsync(id);
            return RedirectToAction(nameof(Index));
        }


        
    }
}