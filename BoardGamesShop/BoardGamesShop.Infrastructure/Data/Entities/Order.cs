using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesShop.Infrastructure.Data.Entities;

public class Order
{
    public int Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))] 
    public ApplicationUser User { get; set; } = null!;

    public List<Game> Games { get;} = new List<Game>();
    public List<GameOrder> GameOrders { get; set; } = new List<GameOrder>();
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public int Count => Games.Count;

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = Games.Sum(g => g.Price);
            return totalPrice - totalPrice * Discount / 100;
        }
    }

    public int Discount { get; set; } = 0;


}