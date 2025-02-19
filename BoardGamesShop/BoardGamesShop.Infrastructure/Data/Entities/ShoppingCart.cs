using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesShop.Infrastructure.Data.Entities;

public class ShoppingCart
{
    public int Id { get; set; }
    
    public Guid UserId { get; set; }

    [Required]
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new HashSet<ShoppingCartItem>();

    public int Count { get; private set; } = 0;

    [Column(TypeName = "decimal(18, 2)")] 
    public decimal TotalPrice { get; private set; } = 0;

    public int Discount { get; set; } = 0;
    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    /// <summary>
    /// Updates the total price and the count of the cart
    /// </summary>
    public void UpdateCart()
    {
        decimal totalPrice = ShoppingCartItems.Sum(si => si.TotalPrice);
        TotalPrice = totalPrice - (totalPrice * Discount / 100);
        Count = ShoppingCartItems.Sum(si => si.Quantity);
    }
}