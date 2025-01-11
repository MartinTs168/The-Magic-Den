using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;

namespace BoardGamesShop.Infrastructure.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(GameNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string ImgUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }
        
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price => OriginalPrice - OriginalPrice * Discount / 100;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Discount { get; set; }
        
        public List<Order> Orders { get; set; } = new List<Order>();
        
        public List<GameOrder> GameOrders { get; set; } = new List<GameOrder>();

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set;} = null!;
        

    }
}
