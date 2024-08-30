using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Models
{
    public class TimeSlotModel
    {
        [Key]
        public int TimeSlotId { get; set;} 
        [ForeignKey("Service")]
        public int ServiceID { get; set; }
        public ServiceModel Service { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndDate { get; set;}
        public bool AvailabilityStatus { get; set; }

    }
}