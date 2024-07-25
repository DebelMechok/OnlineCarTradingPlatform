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
    public class DeleteCategoryModel : PageModel
    {
        private readonly EcommerseProject.Data.CarDealershipContext _context;

        public DeleteCategoryModel(EcommerseProject.Data.CarDealershipContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

                if (category == null)
                {
                    return NotFound();
                }

                Category = category;
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
        var category = await _context.Categories.FindAsync(id);
        var cars = await _context.Cars.Where(c => c.CategoryId == id).ToListAsync();

        if (cars.Count == 0 && category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Internal server error. Please try again later.");
    }
}

    }
}
