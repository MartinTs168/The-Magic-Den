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
    
    public CategoryService(IRepository repository)
    {
        _repository = repository;
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
        
        return category.Id;
    }

    public Task EditAsync(CategoryFormViewModel model, int id)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryFormViewModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}