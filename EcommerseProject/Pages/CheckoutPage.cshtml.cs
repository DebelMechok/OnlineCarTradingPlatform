using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerseProject.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ICartService _cartService;

        public CheckoutModel(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult OnPost()
        {
            return RedirectToPage();
        }
    }
}