using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFood.Models;

public class ReviewModel
{
    [Key]
    public int ReviewId { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public UserModel User { get; set; }
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }
    public RestaurantModel Restaurant { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    
}
