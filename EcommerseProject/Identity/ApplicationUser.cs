using Microsoft.AspNetCore.Identity;
using System;

namespace EcommerseProject.Identity
{
    public class ApplicationUser : IdentityUser
    {
       public string UserRole { get; set; } = string.Empty;
    }
}
