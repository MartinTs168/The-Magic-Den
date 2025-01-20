using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BoardGamesShop.Infrastructure.Constants.AdministratorConstants;

namespace BoardGamesShop.Controllers;

[Authorize(Roles = AdminRole)]
public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;
    
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCategory(CategoryFormViewModel model)
    {
        if (ModelState.IsValid == false)
        {
            return View(model);
        }

        await _categoryService.CreateAsync(model);
        
        return RedirectToAction(nameof(All));
    }
}