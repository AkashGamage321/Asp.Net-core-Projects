using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OnlineFood.Models
{
    public class UserModel:IdentityUser
        {
            [Key]
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public string UserRole { get; set; }
            public ICollection<OrderModel> Orders { get; set; }
            public ICollection<RestaurantModel> Restaurants { get; set; }
            public ICollection<DeliveryModel> Deliveries { get; set; }
        }
}
