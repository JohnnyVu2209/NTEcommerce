namespace NTEcommerce.WebAPI.Model
{
    public class ShoppingCartItem:BaseEntity
    {
        public Guid Id { get; set; }
        public ShoppingCart Cart { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
