using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Client;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BoardGamesShop.Infrastructure.Constants.AdministratorConstants;

namespace BoardGamesShop.Core.Services;

public class UserService : IUserService
{
    private readonly IRepository _repository;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public UserService(IRepository repository,
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _repository = repository;
    }
    
    public async Task<IEnumerable<ClientViewModel>> AllClientsAsync()
    {
        var users = await _repository.AllReadOnly<ApplicationUser>()
            .Where(au => au.IsDeleted == false)
            .ToListAsync();
        var clients = new List<ClientViewModel>();

        foreach (var user in users)
        {
            if (!await _userManager.IsInRoleAsync(user, AdminRole))
            {
                clients.Add(new ClientViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName!,
                    LastName = user.LastName!,
                    Address = user.Address!,
                    Email = user.Email
                });
            }
        }
        
        return clients;
    }

    public async Task DeleteClientAsync(Guid clientId)
    {
        var user = await _repository.GetByIdAsync<ApplicationUser>(clientId);

        if (user != null)
        {
            user.IsDeleted = true;
            await _repository.SaveChangesAsync();
        }
    }

    public async Task<ClientViewModel?> GetClientByIdAsync(Guid clientId)
    {
        var user = await _repository.GetByIdAsync<ApplicationUser>(clientId);

        if (user == null)
        {
            return null;
        }

        return new ClientViewModel()
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName!,
            LastName = user.LastName!,
            Address = user.Address!,
            Email = user.Email
        };

    }

    public async Task<int?> GetUserMagicPointsAsync(Guid userId)
    {
        var user = await _repository.GetByIdAsync<ApplicationUser>(userId);

        if (user == null)
        {
            return null;
        }
        
        return user.MagicPoints;
    }
}