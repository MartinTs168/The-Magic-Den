using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.SubCategory;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;

namespace BoardGamesShop.Core.Services;

public class SubCategoryService : ISubCategoryService
{
    
    private readonly IRepository _repository;
    
    public SubCategoryService(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<int> CreateAsync(SubCategoryViewModel model)
    {
        var subCategory = new SubCategory()
        {
            Name = model.Name
        };
        
        await _repository.AddAsync<SubCategory>(subCategory);
        await _repository.SaveChangesAsync();

        return subCategory.Id;
    }

    public async Task<SubCategoryViewModel?> GetByIdAsync(int id)
    {
        var subCategory = await _repository.GetByIdAsync<SubCategory>(id);
        
        if (subCategory == null)
        {
            return null;
        }

        return new SubCategoryViewModel
        {
            Id = subCategory.Id,
            Name = subCategory.Name
        };
    }

    public async Task EditAsync(SubCategoryViewModel model, int id)
    {
        if (id > 0)
        {
            var subCategory = await _repository.GetByIdAsync<SubCategory>(id);

            if (subCategory!= null)
            {
                subCategory.Name = model.Name;
                await _repository.SaveChangesAsync();
            }
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync<SubCategory>(id);
        await _repository.SaveChangesAsync();
    }
}