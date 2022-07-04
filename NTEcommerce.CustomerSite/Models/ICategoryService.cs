using NTEcommerce.SharedDataModel.Category;
using Refit;

namespace NTEcommerce.CustomerSite.Models;

public interface ICategoryService
{
    [Get("/Category/getList")]
    Task<List<CategoryModel>> GetCategories(CategoryParameters parameters);
}