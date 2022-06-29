using NTEcommerce.SharedDataModel;

namespace NTEcommerce.WebAPI.Services.Interface
{
    public interface ICategoryServices
    {
        Task<CategoryModel> CreateCategory(CreateCategoryModel categoryModel);
    }
}
