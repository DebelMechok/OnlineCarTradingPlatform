namespace EcommerseProject.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Car Car { get; set; } 

    }
}
