using BoardGamesShop.Core.Contracts;
using BoardGamesShop.Core.Models.Client;
using Microsoft.AspNetCore.Mvc;

namespace BoardGamesShop.Areas.Admin.Controllers;

public class ClientController : AdminBaseController
{
    private readonly IClientService _clientService;
    
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }
    
    public async Task<IActionResult> All()
    {
        var clients = await _clientService.AllClientsAsync();
        return View(clients);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _clientService.GetClientByIdAsync(id);
        
        if (user == null)
        {
            return NotFound();
        }
        
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ClientViewModel model)
    {
        await _clientService.DeleteClientAsync(model.Id);
        return RedirectToAction(nameof(All));
    }
}