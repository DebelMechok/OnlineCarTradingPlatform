using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcommerseProject.Data;
using EcommerseProject.Models;
using Microsoft.AspNetCore.Authorization;


namespace EcommerseProject.Pages
{
    
    public class indexModel : PageModel
    {
        private readonly CarDealershipContext _context;

        public indexModel(CarDealershipContext context)
        {
            _context = context;

        }

        public IList<Category> Categories { get; set; } = default!;
        public IList<Car> Cars { get; set; } = default!;

        public IList<ShoppingCartItem> CartItems { get; set; } = new List<ShoppingCartItem>();

        public async Task OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
            Cars = await _context.Cars.ToListAsync();
        }
    }
}
