using BoardGamesShop.Core.Contracts;
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
}