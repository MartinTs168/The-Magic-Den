namespace BoardGamesShop.Core.Models.Game;

public class GameServiceQueryModel
{
    public int TotalGamesCount { get; set; } 
    
    public IEnumerable<GameServiceModel> Games { get; set; } = new List<GameServiceModel>();
}