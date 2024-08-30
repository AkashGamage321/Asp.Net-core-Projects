using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Models
{
    public class PaymentModel
    {
        [Key]
        public int PaymentId { get;set;}
        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public BookingModel Booking { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public decimal AmountPaid { get; set; }
        
    }
}