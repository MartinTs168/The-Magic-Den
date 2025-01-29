namespace BoardGamesShop.Core.Contracts;

public interface IStatisticService
{
    Task<int> CountClientsAsync();
    
    Task<int> CountGamesAsync();
}