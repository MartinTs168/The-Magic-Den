using BoardGamesShop.Core.Enumerations;

namespace BoardGamesShop.Core.Models.Cart;

public class ShoppingCartViewModel
{
    public int Id { get; set; }
    
    public ICollection<ShoppingCartItemServiceModel> ShoppingCartItems { get; set; } = 
        new List<ShoppingCartItemServiceModel>();
    
    public int Count { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public ShoppingCartDiscount Discount { get; set; }
}