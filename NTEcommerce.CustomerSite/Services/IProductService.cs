using NTEcommerce.SharedDataModel.Product;
using Refit;

namespace NTEcommerce.CustomerSite.Services;

[Headers("Content-Type: application/json")]
public interface IProductService
{
    Task<ApiResponse<List<ProductModel>>> GetProducts(ProductParameters parameters);
    Task<ApiResponse<List<ProductModel>>> GetProducts();
    Task<ApiResponse<ProductDetailModel>> GetProduct(Guid id);
    Task<ApiResponse<ProductDetailModel>> AddReview(Guid id, AddProductReviewModel reviewModel);

}
