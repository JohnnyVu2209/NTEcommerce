using NTEcommerce.SharedDataModel.Product;
using Refit;

namespace NTEcommerce.CustomerSite.Services;

[Headers("Content-Type: application/json")]
public interface IProductService
{
    [Get("/api/Product/getListProducts")]
    Task<List<ProductModel>> GetProducts(ProductParameters parameters);

    [Get("/api/Product/getListProducts")]
    Task<List<ProductModel>> GetProducts();

    [Get("/api/Product/getProduct/{id}")]
    Task<ProductDetailModel> GetProduct(Guid id);

}
