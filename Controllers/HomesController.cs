
using Microsoft.AspNetCore.Mvc;
using AspNETcore.BSR.Models;
using AspNETcore.BSR.Services;
using System.Diagnostics;

namespace AspNETcore.BSR.Controllers;

public class HomesController : Controller
{
    private readonly HomeService _homeService;
    private readonly AddressService _addressService;
    private readonly ILogger<HomesController> _logger;

    public HomesController(HomeService homeService, AddressService addressService, ILogger<HomesController> logger)
    {
        _logger = logger;
        _addressService = addressService;
        _homeService = homeService;

    }



    public async Task<IActionResult> GetCities(string state)
    {
        var cities = await _addressService.GetCitiesInState(state);
        return Ok(cities);
    }

    [HttpGet]
    public async Task<IActionResult> AddHomeView()
    {
        var statesResult = await _addressService.GetAmericanStates();

        var addHomeViewModel = new AddHomeViewModel
        {
            States = statesResult,
            Cities = new List<string>()
        };

        return View(addHomeViewModel);
    }

    public async Task<IActionResult> Index(int? minPrice, int? maxPrice, int? minArea, int? maxArea, int? minBath, int? minCar, int? minBed, string? state, string? city, int pageNumber = 1, int pageSize = 12)
    {

        var stopwatch = new Stopwatch(); //added this
        stopwatch.Start(); //added this

        var homesViewModel = new HomesViewModel();

        try
        {


            //modified
            var homes = _homeService.GetHomes(minPrice, maxPrice, minArea, maxArea, minBath, minCar, minBed, state, city);
            int totalItems = homes.Count();
            homes = homes.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            //new
            homesViewModel.States = await _addressService.GetAmericanStates();
            homesViewModel.Homes = homes;
            homesViewModel.PaginationInfo = new PaginationInfo
            {
                CurrentPage = pageNumber,
                ItemsPerPage = pageSize,
                TotalItems = totalItems
            };

            ViewBag.HomesCount = totalItems;

            if (totalItems > 450)
            {
                _logger.LogWarning("Database is close to reaching its capacity");
            }

            _logger.LogInformation("Homes Loaded Correctly");

            ViewBag.HomesCount = totalItems;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching homes from the database: {ex.Message}");
            TempData["ErrorMessage"] = $"Error fetching home from the database: {ex.Message}";
        }

        // Pass the filter values back to the view to retain them
        homesViewModel.MinPrice = minPrice;
        homesViewModel.MaxPrice = maxPrice;
        homesViewModel.MinArea = minArea;
        homesViewModel.MaxArea = maxArea;
        homesViewModel.MinBathrooms = minBath; //new
        homesViewModel.MinGarage = minCar; //new
        homesViewModel.MinBedrooms = minBed;//new
        homesViewModel.State = state;//new
        homesViewModel.City = city;//new

        stopwatch.Stop(); //added this

        ViewBag.LoadTestTime = stopwatch.Elapsed.TotalSeconds.ToString("F4"); //added this

        return View(homesViewModel);
    }


    [HttpPost]
    public IActionResult AddHome(Home newHome)
    {
        if (!ModelState.IsValid)
        {
            return View("AddHomeView", newHome);
        }

        try
        {
            _homeService.AddHome(newHome);
            TempData["SuccessMessage"] = "Home added successfully!";
            return RedirectToAction("Index", "Homes");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error adding home: {ex.Message}";
            return View("AddHomeView", newHome);
        }
    }

    // Update Home

    [HttpGet]
    public IActionResult HomeDetailView(int id)
    {
        var home = _homeService.GetHomeById(id);
        return View(home);
    }

    [HttpPost]
    public IActionResult Update(Home updatedHome)
    {
        if (!ModelState.IsValid)
        {
            return View("HomeDetailView", updatedHome);
        }

        try
        {
            _homeService.UpdateHome(updatedHome);
            TempData["SuccessMessage"] = "Home updated successfully!";
            return RedirectToAction("Index", "Homes");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error updating home: {ex.Message}";
            return View("HomeDetailView", updatedHome);
        }
    }

    // Delete Home

    public IActionResult Delete(int id)
    {
        try
        {
            _homeService.DeleteHome(id);
            TempData["SuccessMessage"] = "Home deleted successfully!";
            return new OkResult();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error deleting home: {ex.Message}";
            return BadRequest(new { message = ex.Message });
        }
    }
}
