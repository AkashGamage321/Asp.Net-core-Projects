using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFood.Models;

public class RestaurantModel
{
    [Key]
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string Contact_Information { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set ;}
    public UserModel User { get; set; }
    public int Opening_Hours { get; set; }
    public ICollection<MenuItemModel> MenuItems { get; set; }
    public ICollection<OrderModel> Orders { get; set; }
}
