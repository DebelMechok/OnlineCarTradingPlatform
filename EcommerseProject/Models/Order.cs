namespace EcommerseProject.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public ShoppingCartItem ShoppingCartItem { get; set; } = new ShoppingCartItem();

    }
}
