using BoardGamesShop.Core.Contracts;
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
}