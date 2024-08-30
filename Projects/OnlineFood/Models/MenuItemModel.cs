using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFood.Models;

public class MenuItemModel
{
    [Key]
    public int MenuItemId { get; set; }
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }
    public RestaurantModel Restaurant { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string ImageUrl { get; set; }
    public string AvailabilityStatus { get; set; }
    public ICollection<OrderItemModel> OrderItems { get; set; }
    public ICollection<OrderModel> Orders { get; set; }
    public PaymentModel Payment { get; set; }
    public DeliveryModel Delivery { get; set; }
}
