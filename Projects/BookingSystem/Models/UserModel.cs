using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Models
{
    public class UserModel:IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ImageUrl { get; set; }
        public string UserRole { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}