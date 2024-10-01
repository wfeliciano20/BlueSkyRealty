using Microsoft.AspNetCore.Identity;

namespace AspNETcore.BSR.Models;

    public class ApplicationUser: IdentityUser
    {
        public DateOnly RegistrationDate { get; set; }
    }
