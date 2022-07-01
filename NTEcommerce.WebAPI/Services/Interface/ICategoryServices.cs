using NTEcommerce.SharedDataModel.Category;
using NTEcommerce.WebAPI.Constant;
using NTEcommerce.WebAPI.Model;

namespace NTEcommerce.WebAPI.Services.Interface
{
    public interface ICategoryServices
    {
        Task<CategoryModel> CreateCategory(CreateCategoryModel categoryModel);
        Task<PagedList<Category,CategoryModel>?> GetList(CategoryParameters parameters);
        Task<CategoryDetailModel?> GetCategory(Guid id);
    }
}
