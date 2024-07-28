
using Microsoft.AspNetCore.Mvc;
using AspNETcore.BSR.Models;
using AspNETcore.BSR.Services;

namespace AspNETcore.BSR.Controllers;

public class HomesController : Controller
{
    private readonly HomeService _homeService;

    public HomesController(HomeService homeService)
    {
        _homeService = homeService;
    }

    public IActionResult Index()
    {
        var homesViewModel = new HomesViewModel();

        try
        {
            homesViewModel.Homes = _homeService.GetHomes();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error fetching home from the database: {ex.Message}";
        }

        return View(homesViewModel);
    }
}
