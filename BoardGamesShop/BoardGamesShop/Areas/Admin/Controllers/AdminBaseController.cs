using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BoardGamesShop.Infrastructure.Constants.AdministratorConstants;

namespace BoardGamesShop.Areas.Admin.Controllers;

[Area(AdminAreaName)]
[Authorize(Roles = AdminRole)]
public class AdminBaseController : Controller
{
    
}