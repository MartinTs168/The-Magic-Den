using BoardGamesShop.Core.Models.Client;

namespace BoardGamesShop.Core.Contracts;

public interface IStatisticService
{
    Task<IEnumerable<AllClientsVIewModel>> AllClientsAsync();
}