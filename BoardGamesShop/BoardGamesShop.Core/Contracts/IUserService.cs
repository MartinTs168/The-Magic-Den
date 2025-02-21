using BoardGamesShop.Core.Models.Client;

namespace BoardGamesShop.Core.Contracts;

public interface IUserService
{
    Task<IEnumerable<ClientViewModel>> AllClientsAsync();
    Task DeleteClientAsync(Guid clientId);
    
    Task<ClientViewModel?> GetClientByIdAsync(Guid clientId);

    Task<int?> GetUserMagicPointsAsync(Guid userId);
}