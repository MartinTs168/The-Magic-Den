using System.ComponentModel.DataAnnotations;
using BoardGamesShop.Core.Enumerations;

namespace BoardGamesShop.Core.Models.Game;

public class AllGamesQueryModel
{
    public int GamesPerPage { get; } = 20; // TODO: this is 5 for test purposes change to more in the future

    [Display(Name = "Sub Category")]
    public string SubCategory { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Brand { get; set; } = null!;

    [Display(Name = "Search")]
    public string SearchTerm { get; set; } = null!;
    
    public GameSorting Sort { get; set; }
    
    public int CurrentPage { get; set; } = 1;

    public IEnumerable<string> SubCategories { get; set; } = null!;
    public IEnumerable<string> Brands { get; set; } = null!;
    
    public IEnumerable<string> Categories { get; set; } = null!;

    public List<string> SelectedBrands { get; set; } = new List<string>();
    
    public int TotalGamesCount { get; set; }

    public IEnumerable<GameServiceModel> Games { get; set; } = new List<GameServiceModel>();
}