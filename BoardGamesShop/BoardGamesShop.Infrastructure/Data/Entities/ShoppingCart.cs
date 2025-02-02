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

    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
    
    public int Count { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalPrice { get; private set; }

    public int Discount { get; set; } = 0;
    
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
}