using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Models;

namespace BookingSystem.Services
{
    public class PaymentService:IPaymentService
    {
        // Example: Using an in-memory list for demo purposes. Replace with your database context.
        private readonly List<PaymentModel> _payments = new List<PaymentModel>();

        public async Task<IEnumerable<PaymentModel>> GetAllPaymentsAsync()
        {
            return await Task.FromResult(_payments);
        }

        public async Task<PaymentModel> GetPaymentByIdAsync(int id)
        {
            var payment = _payments.FirstOrDefault(p => p.PaymentId == id);
            return await Task.FromResult(payment);
        }

        public async Task ProcessPaymentAsync(PaymentModel paymentModel)
        {
            paymentModel.PaymentId = _payments.Count + 1;
            _payments.Add(paymentModel);
            await Task.CompletedTask;
        }

        public async Task UpdatePaymentAsync(PaymentModel paymentModel)
        {
            var payment = _payments.FirstOrDefault(p => p.PaymentId == paymentModel.PaymentId);
            if (payment != null)
            {
                payment.AmountPaid = paymentModel.AmountPaid;
                payment.PaymentDate = paymentModel.PaymentDate;
                // Update other properties as needed
            }
            await Task.CompletedTask;
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = _payments.FirstOrDefault(p => p.PaymentId == id);
            if (payment != null)
            {
                _payments.Remove(payment);
            }
            await Task.CompletedTask;
        }
    }
}