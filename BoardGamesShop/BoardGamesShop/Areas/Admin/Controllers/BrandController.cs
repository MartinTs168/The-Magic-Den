using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Brand;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Areas.Admin.Controllers;

public class BrandController : AdminBaseController
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

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var brand = await _brandService.GetByIdAsync(id);

        if (brand == null)
        {
            return NotFound();
        }

        return View(brand);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BrandModel model, int id)
    {
        if (id <= 0 || ModelState.IsValid == false)
        {
            return View(model);
        }

        await _brandService.EditAsync(model, id);
        
        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        var brand = await _brandService.GetByIdAsync(id);

        if (brand == null)
        {
            return NotFound();
        }

        return View(brand);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(BrandModel model)
    {
        await _brandService.DeleteAsync(model.Id);
        
        return RedirectToAction(nameof(All));
    }
}