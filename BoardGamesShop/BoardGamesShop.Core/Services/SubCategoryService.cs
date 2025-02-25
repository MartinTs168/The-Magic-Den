using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Game;
using BoardGamesShop.Core.Models.SubCategory;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.Core.Services;

public class SubCategoryService : ISubCategoryService
{
    
    private readonly IRepository _repository;
    
    public SubCategoryService(IRepository repository)
    {
        _repository = repository;
    }

    public async  Task<IEnumerable<GameSubCategoryServiceModel>> AllAsync()
    {
        return await _repository.AllReadOnly<SubCategory>()
            .Select(sc => new GameSubCategoryServiceModel()
            {
                Id = sc.Id,
                Name = sc.Name,
            }).ToListAsync();
    }

    public async Task<int> CreateAsync(SubCategoryViewModel model)
    {
        var subCategory = new SubCategory()
        {
            Name = model.Name,
            CategoryId = model.CategoryId
        };
        
        await _repository.AddAsync(subCategory);
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
            Name = subCategory.Name,
            CategoryId = subCategory.CategoryId
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
                subCategory.CategoryId = model.CategoryId;
                await _repository.SaveChangesAsync();
            }
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync<SubCategory>(id);
        await _repository.SaveChangesAsync();
    }

    public async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await _repository.AllReadOnly<Category>()
            .AnyAsync(c => c.Id == categoryId);
    }

    public async Task<IEnumerable<SubCategoryCategoriesServiceModel>> AllCategoriesAsync()
    {
        return await _repository.AllReadOnly<Category>()
            .Select(c => new SubCategoryCategoriesServiceModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();
    }

    public async Task<IEnumerable<string>> GetSubCategoriesNamesByCategoryNameAsync(string categoryName)
    {
        return await _repository.AllReadOnly<SubCategory>()
            .Where(sc => sc.Category.Name == categoryName)
            .Select(sc => sc.Name)
            .ToListAsync();
    }
}