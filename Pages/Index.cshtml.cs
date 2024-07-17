using AspNETcore.BSR.Models;
using AspNETcore.BSR.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class IndexModel : PageModel
    {
        private readonly HomeService _homeService;
        public List<Home> Homes { get; private set; }
        public decimal ThresholdPrice { get; set; }

        public IndexModel(HomeService homeService)
        {
            _homeService = homeService;
        }

        public void OnGet()
        {
            Homes = _homeService.GetHomes();
            ThresholdPrice = 400000;
        }
    }
}
