using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Models;

namespace BookingSystem.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentModel>> GetAllPaymentsAsync();
        Task<PaymentModel> GetPaymentByIdAsync(int id);
        Task ProcessPaymentAsync(PaymentModel paymentModel);
        Task UpdatePaymentAsync(PaymentModel paymentModel);
        Task DeletePaymentAsync(int id);
        
    }
}