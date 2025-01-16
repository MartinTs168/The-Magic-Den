using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Brand;
using BoardGamesShop.Infrastructure.Data.Entities;
using BoardGamesShop.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BoardGamesShop.Core.Services;

public class BrandService : IBrandService
{

    private readonly IRepository _repository;
    
    public BrandService(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<BrandModel>> AllAsync()
    {
        return await _repository.AllReadOnly<Brand>()
            .Select(b => new BrandModel { Id = b.Id, Name = b.Name })
            .ToListAsync();

    }

    
}