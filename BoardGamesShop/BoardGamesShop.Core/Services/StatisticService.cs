using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Client;
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
    
    public async Task<IEnumerable<AllClientsVIewModel>> AllClientsAsync()
    {
        var users = await _repository.AllReadOnly<ApplicationUser>()
            .Where(au => au.IsDeleted == false)
            .ToListAsync();
        var clients = new List<AllClientsVIewModel>();

        foreach (var user in users)
        {
            if (!await _userManager.IsInRoleAsync(user, AdminRole))
            {
                clients.Add(new AllClientsVIewModel()
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
}