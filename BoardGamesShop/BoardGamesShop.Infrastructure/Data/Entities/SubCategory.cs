using System.ComponentModel.DataAnnotations;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;

namespace BoardGamesShop.Infrastructure.Data.Entities;

public class SubCategory
{
    public int Id { get; set; }

    [Required]
    [MaxLength(CategoryNameMaxLength)]
    public string Name { get; set; } = null!;
    
    [Required]
    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
    
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}