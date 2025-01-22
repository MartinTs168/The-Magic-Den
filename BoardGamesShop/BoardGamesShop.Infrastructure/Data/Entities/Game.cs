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
        
        
        public decimal Price => OriginalPrice - OriginalPrice * Discount / 100;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Discount { get; set; }

        [Required]
        [MaxLength(GameAgeRatingMaxLength)]
        public string AgeRating { get; set; } = null!;
        
        [Required]
        public int NumberOfPlayers { get; set; }
        
        public List<Order> Orders { get; set; } = new List<Order>();
        
        public List<GameOrder> GameOrders { get; set; } = new List<GameOrder>();

        [Required]
        public int BrandId { get; set; }
        
        public int? SubCategoryId { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        public SubCategory? SubCategory { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set;} = null!;
        

    }
}
