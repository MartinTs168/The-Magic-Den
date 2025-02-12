namespace BoardGamesShop.Core.Contracts;

public interface IStatisticService
{
    Task<int> CountClientsAsync();
    
    Task<int> CountGamesAsync();

    Task<int> CountOrdersAsync();

    Task<decimal> TotalEarningsAsync();
}