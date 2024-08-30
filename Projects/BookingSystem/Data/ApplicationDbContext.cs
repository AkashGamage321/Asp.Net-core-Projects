using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options)
        :base(options)
        {

        }
        public DbSet<ServiceModel> Services {get; set; }
        public DbSet<TimeSlotModel> TimeSlots { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }
        
        
    }
}