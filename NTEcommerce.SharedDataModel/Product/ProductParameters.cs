namespace NTEcommerce.SharedDataModel.Product
{
    public class ProductParameters: QueryStringParameters
    {
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
    }
}
