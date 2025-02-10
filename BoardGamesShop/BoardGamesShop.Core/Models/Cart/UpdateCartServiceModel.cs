using System.ComponentModel.DataAnnotations;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;

namespace BoardGamesShop.Core.Models.Cart;

public class UpdateCartServiceModel
{
    public int GameId { get; set; }
    
    [Range(typeof(int), ShoppingCartItemMinQuantity, ShoppingCartItemMaxQuantity)]
    public int Quantity { get; set; }
}