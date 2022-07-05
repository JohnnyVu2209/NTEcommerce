namespace NTEcommerce.SharedDataModel.Category
{
    public class CategoryParameters: QueryStringParameters
    {
        public CategoryParameters()
        {
            OrderBy = "UpdatedDate";
        }
        public string? Name { get; set; }
    }
}
