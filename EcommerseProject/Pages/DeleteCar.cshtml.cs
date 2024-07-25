using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcommerseProject.Data;
using EcommerseProject.Models;

namespace EcommerseProject.Pages
{
    public class DeleteCarModel : PageModel
    {
        private readonly EcommerseProject.Data.CarDealershipContext _context;

        public DeleteCarModel(EcommerseProject.Data.CarDealershipContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Car Car { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(m => m.Id == id);

                if (car == null)
                {
                    return NotFound();
                }

                Car = car;
                return Page();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var car = await _context.Cars.FindAsync(id);

                if (car == null)
                {
                    return NotFound();
                }

                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

    }
}
