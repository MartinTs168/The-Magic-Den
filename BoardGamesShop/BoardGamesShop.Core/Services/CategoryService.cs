using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Category;
using BoardGamesShop.Core.Models.SubCategory;
using BoardGamesShop.Infrastructure.Data.Common;
using BoardGamesShop.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShop.Core.Services;

public class CategoryService : ICategoryService
{
    
    private readonly IRepository _repository;
    private readonly ICacheCategoriesService _cacheCategories;
    
    public CategoryService(
        IRepository repository,
        ICacheCategoriesService cacheCategories)
    {
        _repository = repository;
        _cacheCategories = cacheCategories;
    }
    public async Task<IEnumerable<AllCategoriesViewModel>> AllAsync()
    {
        return await _repository.AllReadOnly<Category>()
            .Select(c => new AllCategoriesViewModel
            {
                Id = c.Id,
                Name = c.Name,
                SubCategories = c.SubCategories.Select(sc => new SubCategoryViewModel()
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    
                }).ToList() 
            }).ToListAsync();
    }

    public async Task<int> CreateAsync(CategoryFormViewModel model)
    {
        var category = new Category()
        {
            Name = model.Name
        };
        
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();
        
        _cacheCategories.InvalidateCache();
        return category.Id;
    }

    public async Task EditAsync(CategoryFormViewModel model, int id)
    {
        if (id > 0)
        {
            var categoryObj = await _repository.GetByIdAsync<Category>(id);

            if (categoryObj!= null)
            {
                categoryObj.Name = model.Name;
                
                _cacheCategories.InvalidateCache();
                await _repository.SaveChangesAsync();
            }
        }
    }

    public async Task<CategoryFormViewModel?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync<Category>(id);
        
        if (category == null)
        {
            return null;
        }

        return new CategoryFormViewModel
        {
            Id = category.Id,
            Name = category.Name
        };

    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync<Category>(id);
        await _repository.SaveChangesAsync();
        
        _cacheCategories.InvalidateCache();
    }
}