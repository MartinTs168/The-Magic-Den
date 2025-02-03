using System.ComponentModel.DataAnnotations;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;

namespace BoardGamesShop.Core.Models.Cart;

public class ShoppingCartItemServiceModel
{
    [Required] 
    public int GameId { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public string ImgUrl { get; set; } = null!;
    
    
    [Required]
    [Range(typeof(decimal), ShoppingCartItemMinQuantity, ShoppingCartItemMaxQuantity)]
    public int Quantity { get; set; }

    [Required]
    public decimal TotalPrice { get; set; }
}