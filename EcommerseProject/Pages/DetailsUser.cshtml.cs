using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcommerseProject.Data;
using EcommerseProject.Models;
using EcommerseProject.Identity;

namespace EcommerseProject.Pages
{
    public class DetailsUserModel : PageModel
    {
        private readonly EcommerseProject.Data.CarDealershipContext _context;

        public DetailsUserModel(EcommerseProject.Data.CarDealershipContext context)
        {
            _context = context;
        }

        public ApplicationUser ApplicationUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            else
            {
                ApplicationUser = applicationUser;
            }
            return Page();
        }
    }
}
