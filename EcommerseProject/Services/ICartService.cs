using EcommerseProject.Models;
using System.Collections.Generic;

namespace EcommerseProject.Services
{
    public interface ICartService
    {
        List<ShoppingCartItem> GetCart();
        void AddToCart(ShoppingCartItem item);
        void RemoveFromCart(int carId);
        int GetCartItemCount();
        decimal GetTotalPrice();
        void ClearCart();
    }
}
