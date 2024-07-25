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
using EcommerseProject.Services;

namespace EcommerseProject.Pages
{
    
    public class indexModel : PageModel
    {
        private readonly CarDealershipContext _context;
        private readonly ICartService _cartService;

        public indexModel(CarDealershipContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public IList<Category> Categories { get; set; } = default!;
        public IList<Car> Cars { get; set; } = default!;

        public IList<ShoppingCartItem> CartItems { get; set; } = new List<ShoppingCartItem>();

        public async Task OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
            Cars = await _context.Cars.ToListAsync();
            CartItems = _cartService.GetCart() ?? new List<ShoppingCartItem>();
        }

        public IActionResult OnPostAddToCart(int carId, int quantity)
        {
            var car = _context.Cars.Find(carId);
            if (car != null)
            {
                var item = new ShoppingCartItem
                {
                    CarId = carId,
                    Price = car.Price,
                    Quantity = quantity,
                    Car = car
                };

                _cartService.AddToCart(item);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFromCart(int carId)
        {
            
            _cartService.RemoveFromCart(carId);
            return RedirectToPage();
        }
    }
}
