using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesShop.Infrastructure.Data.Entities;

public class OrderItem
{
    public int OrderId { get; set; }
    
    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }
    
    [Required]
    public int GameId { get; set; }

    [Required]
    [ForeignKey(nameof(OrderId))]
    public Game Game { get; set; } = null!;
    
    [Required]
    public int Quantity { get; set; }
}