using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFood.Models;

public class DeliveryModel
{
    [Key]
    public int DeliveryId { get; set; }
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public OrderModel Order {get; set; }
    public string DeliveryAddress { get; set; }
    public DateTime DeliveryDate  { get; set; }
    public string DeliveryStatus { get; set; }
    [ForeignKey("User")]
    public int UserId { get ;set; }
    public UserModel User { get; set; }
}
