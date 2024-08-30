using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFood.Models;

public class OrderModel
{
    [Key]
    public int OrderId { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public UserModel User { get; set; }
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }
    public RestaurantModel Restaurant { get; set; }

    [ForeignKey("MenuItem")]
    public int MenuItemId { get; set; }
    public MenuItemModel MenuItem { get; set; }
    public DateTime OrderDateTime { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentStatus { get; set;}
    public string DeliveryAddress { get; set; }
}
