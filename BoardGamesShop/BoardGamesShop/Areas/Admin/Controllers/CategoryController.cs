using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Category;
using BoardGamesShop.Core.Models.SubCategory;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Areas.Admin.Controllers;

public class CategoryController : AdminBaseController
{
    private readonly ICategoryService _categoryService;
    private readonly ISubCategoryService _subcategoryService;
    
    public CategoryController(ICategoryService categoryService,
        ISubCategoryService subcategoryService)
    {
        _categoryService = categoryService;
        _subcategoryService = subcategoryService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var categories = await _categoryService.AllAsync();
        return View(categories);
    }

    [HttpGet]
    public async Task<IActionResult> CreateCategory()
    {
        var model = new CategoryFormViewModel();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryFormViewModel model)
    {
        if (ModelState.IsValid == false)
        {
            return View(model);
        }

        await _categoryService.CreateAsync(model);
        
        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> EditCategory(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }
        
        var category = await _categoryService.GetByIdAsync(id);
        
        if (category == null)
        {
            return NotFound();
        }
        
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(CategoryFormViewModel model, int id)
    {
        if (id <= 0 || ModelState.IsValid == false)
        {
            return View(model);
        }
        
        await _categoryService.EditAsync(model, id);
        
        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }
        
        var category = await _categoryService.GetByIdAsync(id);
        
        if (category == null)
        {
            return NotFound();
        }
        
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory(CategoryFormViewModel model)
    {
        await _categoryService.DeleteAsync(model.Id);
        
        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> CreateSubCategory()
    {
        var model = new SubCategoryViewModel()
        {
            Categories = await _subcategoryService.AllCategoriesAsync()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubCategory(SubCategoryViewModel model)
    {
        if (await _subcategoryService.CategoryExistsAsync(model.CategoryId) == false)
        {
            ModelState.AddModelError(nameof(model.CategoryId), "");
        }
        
        
        if (ModelState.IsValid == false)
        {
            model.Categories = await _subcategoryService.AllCategoriesAsync();
            return View(model);
        }

        await _subcategoryService.CreateAsync(model);
        
        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> EditSubCategory(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }
        
        var subcategory = await _subcategoryService.GetByIdAsync(id);
        
        if (subcategory == null)
        {
            return NotFound();
        }
        
        subcategory.Categories = await _subcategoryService.AllCategoriesAsync();
        return View(subcategory);
    }

    [HttpPost]
    public async Task<IActionResult> EditSubCategory(SubCategoryViewModel model, int id)
    {
        if(id <= 0) return NotFound();
        
        if (await _subcategoryService.CategoryExistsAsync(model.CategoryId) == false)
        {
            ModelState.AddModelError(nameof(model.CategoryId), "");
        }

        if (ModelState.IsValid == false)
        {
            model.Categories = await _subcategoryService.AllCategoriesAsync();
            return View(model);
        }
        
        await _subcategoryService.EditAsync(model, id);
        
        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> DeleteSubCategory(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }
        
        var subcategory = await _subcategoryService.GetByIdAsync(id);
        
        if (subcategory == null)
        {
            return NotFound();
        }

        subcategory.Categories = await _subcategoryService.AllCategoriesAsync();
        
        return View(subcategory);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSubCategory(SubCategoryViewModel model)
    {
        await _subcategoryService.DeleteAsync(model.Id);
        
        return RedirectToAction(nameof(All));
    }
}