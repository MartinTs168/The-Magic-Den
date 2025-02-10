using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;

namespace BoardGamesShop.Infrastructure.Data.Entities;

public class Order
{
    public int Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))] 
    public virtual ApplicationUser User { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public int Count { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalPrice
    {
        get;
        private set;
    }

    public int Discount { get; set; } = 0;

    [Required]
    [MaxLength(AddressMaxLength)]
    public string Address { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}