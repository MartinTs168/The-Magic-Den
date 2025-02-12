using System.ComponentModel.DataAnnotations;

namespace BoardGamesShop.Core.Models.Order;

public class OrderViewModel
{
    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    [Display(Name = "User")]
    public string UserName { get; set; } = null!;
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public int Discount { get; set; }
    
    [Required]
    [Display(Name = "Total Price")]
    public decimal TotalPrice { get; set; }
}