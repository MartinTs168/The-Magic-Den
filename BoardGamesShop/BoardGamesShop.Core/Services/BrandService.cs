using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Brand;
using BoardGamesShop.Infrastructure.Data.Entities;
using BoardGamesShop.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;

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
            .Select(b => new BrandModel
            {
                Id = b.Id, 
                Name = b.Name
            })
            .ToListAsync();

    }

    public async Task<int> CreateAsync(BrandModel model)
    {
        var brand = new Brand
        {
            Name = model.Name
        };

        await _repository.AddAsync(brand);
        await _repository.SaveChangesAsync();

        return brand.Id;
    }
}