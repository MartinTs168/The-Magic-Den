using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Brand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BoardGamesShop.Infrastructure.Constants.AdministratorConstants;

namespace BoardGamesShop.Controllers;

[Authorize(Roles = AdminRole)]
public class BrandController : BaseController
{
    private readonly IBrandService _brandService;
    
    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var brands = await _brandService.AllAsync();
        return View(brands);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new BrandModel();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(BrandModel model)
    {
        if (ModelState.IsValid == false)
        {
            return View(model);
        }

        await _brandService.CreateAsync(model);
        
        return RedirectToAction(nameof(All));
    }
}