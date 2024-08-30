using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFood.Models;

public class OrderItemModel
{
    [Key]
    public int OrderItemId { get; set; }
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public OrderModel Order { get; set; }
    [ForeignKey("MenuItem")]
    public int MenuItemId { get ; set; }
    public MenuItemModel MenuItem { get; set; }
    public int Quantity { get ;set; }
    public decimal Price { get; set; }
}
