using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerseProject.Data;
using EcommerseProject.Models;

namespace EcommerseProject.Pages
{
    public class EditCarModel : PageModel
    {
        private readonly EcommerseProject.Data.CarDealershipContext _context;

        public EditCarModel(EcommerseProject.Data.CarDealershipContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Car Car { get; set; } = default!;

        [BindProperty]
        public string CategoryName { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var car =  await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == car.CategoryId);
            if (car == null)
            {
                return NotFound();
            }
            Car = car;
            CategoryName = category.CategoryName;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c=>c.CategoryName == CategoryName);
            if (category == null)
            {
                ModelState.AddModelError("CategoryName", "Invalid Category.");
                return Page();
            }
            Car.CategoryId = category.CategoryId;
            if (!ModelState.IsValid)
            {
                
                return Page();
            }

            
            _context.Attach(Car).State = EntityState.Modified;

            try
            {              
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
                if (!CarExists(Car.Id))
                {
                    
                    return NotFound("The car you are trying to update no longer exists. It might have been deleted by another user.");
                }
                else
                {
                    
                    throw new InvalidOperationException("A concurrency error occurred while trying to update the car. Please try again.");
                }
            }

            
            return RedirectToPage("./Index");
        }

        
        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

    }
}
