namespace NTEcommerce.WebAPI.Constant
{
    public class ProductParameters: QueryStringParameters
    {
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
    }
}
