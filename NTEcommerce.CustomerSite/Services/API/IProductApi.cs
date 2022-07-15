using NTEcommerce.SharedDataModel.Product;
using Refit;

namespace NTEcommerce.CustomerSite.Services.API
{
    [Headers("Content-Type: application/json")]
    public interface IProductApi
    {
        [Get("/api/Product/getListProducts")]
        Task<ApiResponse<List<ProductModel>>> GetProducts(ProductParameters parameters);
        [Get("/api/Product/getListProducts")]
        Task<ApiResponse<List<ProductModel>>> GetProducts();
        [Get("/api/Product/getProduct/{id}")]
        Task<ApiResponse<ProductDetailModel>> GetProduct(Guid id);
        [Post("/api/Product/addReview/{ProductId}")]
        Task<ApiResponse<ProductDetailModel>> AddReview(Guid ProductId, AddProductReviewModel model);
    }
}
