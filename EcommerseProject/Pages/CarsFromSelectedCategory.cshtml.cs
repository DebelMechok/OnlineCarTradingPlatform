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
    public class CarsFromSelectedCategoryModel : PageModel
    {
        private readonly EcommerseProject.Data.CarDealershipContext _context;

        public CarsFromSelectedCategoryModel(EcommerseProject.Data.CarDealershipContext context)
        {
            _context = context;
        }

        public IList<Car> Cars { get;set; } = default!;
        public string Category {  get; set; }

        public int Id { get; set; }
        public async Task OnGetAsync(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                ModelState.AddModelError(string.Empty, "Invalid category ID.");
                return;
            }

            try
            {
                
                Cars = await _context.Cars.Where(c => c.CategoryId == id).ToListAsync();
                var category = _context.Categories.Where(c=>c.CategoryId == id).FirstOrDefault();

                Id = category.CategoryId;
                Category = category.CategoryName;
            }
            catch (Exception ex)
            {               
                ModelState.AddModelError(string.Empty, "An error occurred while fetching cars. Please try again later.");
            }
        }
    }
}
