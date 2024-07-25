using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EcommerseProject.Data;
using EcommerseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerseProject.Pages
{
    public class CreateCarModel : PageModel
    {
        private readonly EcommerseProject.Data.CarDealershipContext _context;

        public CreateCarModel(EcommerseProject.Data.CarDealershipContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Car Car { get; set; } = default!;

        [BindProperty]
        public string CategoryName { get; set; }

        public int Id { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
               
                var category = _context.Categories.SingleOrDefault(c => c.CategoryName == CategoryName);

                
                if (category == null)
                {
                    ModelState.AddModelError("CategoryName", "Invalid Category.");
                    return Page();
                }

                
                Car.CategoryId = category.CategoryId;

                
                _context.Cars.Add(Car);
                await _context.SaveChangesAsync();

                
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while adding the car. Please try again later.");
                return Page();
            }
        }
    }
}
