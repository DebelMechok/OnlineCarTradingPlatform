using Microsoft.EntityFrameworkCore;
using EcommerseProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EcommerseProject.Identity;


namespace EcommerseProject.Data
{
    public class CarDealershipContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public CarDealershipContext(DbContextOptions<CarDealershipContext> options)
            : base(options)
        {
        }
    }
}
