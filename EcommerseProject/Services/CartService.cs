using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EcommerseProject.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using EcommerseProject.Services;
using EcommerseProject.Identity;
using Microsoft.AspNetCore.Identity;


namespace EcommerseProject
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private const string CartSessionKey = "Cart";

        public CartService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        private string GetCartKey()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return $"{CartSessionKey}_{userId}";
        }

        public List<ShoppingCartItem> GetCart()
        {
            var cartKey = GetCartKey();
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = session.GetString(cartKey);

            if (cart == null)
            {
                return new List<ShoppingCartItem>();
            }

            return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cart);
        }

        public void AddToCart(ShoppingCartItem item)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(c => c.CarId == item.CarId);

            if (existingItem != null)
            {
                existingItem.Quantity+=item.Quantity;
            }
            else
            {
                cart.Add(new ShoppingCartItem { CarId = item.CarId,Car = item.Car, Price = item.Price, Quantity = item.Quantity });
            }

            SaveCart(cart);
        }

        public void RemoveFromCart(int carId)
        {
            var cart = GetCart();
            var itemToRemove = cart.FirstOrDefault(item => item.CarId == carId);

            if (itemToRemove != null)
            {
                if(itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity--;
                }
                else
                {
                    cart.Remove(itemToRemove);
                }      
            }

            SaveCart(cart);
        }

        public int GetCartItemCount()
        {
            return GetCart().Sum(item => item.Quantity);
        }

        public decimal GetTotalPrice()
        {
            return GetCart().Sum(item => item.Price * item.Quantity);
        }

        public void ClearCart()
        {
            var cartKey = GetCartKey();
            _httpContextAccessor.HttpContext.Session.Remove(cartKey);
        }

        private void SaveCart(List<ShoppingCartItem> cart)
        {
            var cartKey = GetCartKey();
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString(cartKey, JsonConvert.SerializeObject(cart));
        }
    }
}
