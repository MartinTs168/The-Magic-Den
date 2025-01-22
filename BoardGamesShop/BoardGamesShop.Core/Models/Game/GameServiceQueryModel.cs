namespace BoardGamesShop.Core.Models.Game;

public class GameServiceQueryModel
{
    public int TotalProductsCount { get; set; } 
    
    public IEnumerable<GameServiceModel> Products { get; set; } = new List<GameServiceModel>();
}