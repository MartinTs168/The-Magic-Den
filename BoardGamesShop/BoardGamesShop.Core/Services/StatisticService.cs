using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BoardGamesShop.Infrastructure.Constants.AdministratorConstants;

namespace BoardGamesShop.Core.Services;

public class StatisticService : IStatisticService
{
    private readonly IRepository _repository;
    private readonly UserManager<ApplicationUser> _userManager;

    public StatisticService(IRepository repository,
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _repository = repository;
    }

    public async Task<int> CountClientsAsync()
    {
        var users = await _repository.AllReadOnly<ApplicationUser>()
            .Where(au => au.IsDeleted == false)
            .ToListAsync();

        int clientCount = 0;

        foreach (var user in users)
        {
            if (!await _userManager.IsInRoleAsync(user, AdminRole))
            {
                clientCount++;
            }
        }
        
        return clientCount;
    }

    public async Task<int> CountGamesAsync()
    {
        return await _repository.AllReadOnly<Game>()
            .CountAsync();
    }
}