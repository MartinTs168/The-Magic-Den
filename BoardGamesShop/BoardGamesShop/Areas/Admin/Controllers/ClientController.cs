using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Client;
using static BoardGamesShop.Core.Constants.MessageConstants;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Areas.Admin.Controllers;

public class ClientController : AdminBaseController
{
    private readonly IUserService _userService;
    
    public ClientController(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<IActionResult> All()
    {
        var clients = await _userService.AllClientsAsync();
        return View(clients);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _userService.GetClientByIdAsync(id);
        
        if (user == null)
        {
            return NotFound();
        }
        
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ClientViewModel model)
    {
        await _userService.DeleteClientAsync(model.Id);
        TempData[UserMessageSuccess] = "User deleted successfully";
        return RedirectToAction(nameof(All));
    }
}