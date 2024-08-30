using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Models
{
    public class BookingModel
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserModel User { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public ServiceModel Service { get; set; }
        [ForeignKey("TimeSlot")]
        public int TimeSlotId { get; set; }
        public TimeSlotModel TimeSlot { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public string PaymentStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public string BookingStatus { get; set ;}
    }
}