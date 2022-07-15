using NTEcommerce.CustomerSite.Services.API;
using NTEcommerce.SharedDataModel.Product;
using Refit;

namespace NTEcommerce.CustomerSite.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductApi productApi;
        public ProductService(IProductApi productApi)
        {
            this.productApi = productApi;
        }

        public async Task<ApiResponse<ProductDetailModel>> AddReview(Guid id, AddProductReviewModel reviewModel)
        {
            return await productApi.AddReview(id,reviewModel);
        }

        public async Task<ApiResponse<ProductDetailModel>> GetProduct(Guid id)
        {
            return await productApi.GetProduct(id);
        }

        public async Task<ApiResponse<List<ProductModel>>> GetProducts(ProductParameters parameters)
        {
            return await productApi.GetProducts(parameters);
        }

        public async Task<ApiResponse<List<ProductModel>>> GetProducts()
        {
            return await productApi.GetProducts();
        }
    }
}
