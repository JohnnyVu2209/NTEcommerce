using NTEcommerce.SharedDataModel;
using NTEcommerce.SharedDataModel.Product;
using NTEcommerce.WebAPI.Constant;
using NTEcommerce.WebAPI.Model;

namespace NTEcommerce.WebAPI.Services.Interface
{
    public interface IProductServices
    {
        Task<ProductModel> CreateProduct(CreateProductModel productModel);
        Task<PagedList<Product,ProductModel>> GetList(ProductParameters parameters);
        Task<ProductDetailModel> GetProduct(Guid id);
        Task<ProductDetailModel?> AddReview(Guid productId, AddProductReviewModel reviewModel);
    }
}
