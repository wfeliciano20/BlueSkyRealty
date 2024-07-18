using AspNETcore.BSR.Models;
using AspNETcore.BSR.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNETcore.BSR.Pages
{
    public class AddHomeModel : PageModel
    {
        private readonly HomeService _homeService;

        public AddHomeModel(HomeService homeService)
        {
            _homeService = homeService;
        }

        [BindProperty]
        public Home NewHome { get; set; }

        public void OnGet()
        {
        }

    try
    {
        _homeService.AddHome(NewHome);
        TempData["SuccessMessage"] = "Home added successfully!";
        return RedirectToPage("Index");
    }
    catch (Exception ex)
    {
        TempData["ErrorMessage"] = $"Error adding home: {ex.Message}";
        return Page();
}
    }
}
