using AutoMapper;
using System.Text;
using System.Reflection;
using NTEcommerce.WebAPI.Model;
using System.Linq.Dynamic.Core;
using NTEcommerce.SharedDataModel;
using Microsoft.EntityFrameworkCore;
using NTEcommerce.WebAPI.Exceptions;
using NTEcommerce.SharedDataModel.Category;
using NTEcommerce.WebAPI.Services.Interface;
using NTEcommerce.WebAPI.Repository.Interface;
using static NTEcommerce.WebAPI.Constant.MessageCode;

namespace NTEcommerce.WebAPI.Services.Implement
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CategoryServices> logger;
        private readonly IMapper mapper;
        public CategoryServices(IUnitOfWork unitOfWork, ILogger<CategoryServices> logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<CategoryModel> CreateCategory(CreateCategoryModel categoryModel)
        {
            if (await unitOfWork.Category.CheckExistName(categoryModel.Name) == true)
                throw new BadRequestException(ErrorCode.CATEGORY_NAME_ALREADY_EXISTED);

            var newCategory = mapper.Map<Category>(categoryModel);

            if (categoryModel.ParentId != null)
            {
                var categoryParent = await unitOfWork.Category.GetById((Guid)categoryModel.ParentId);
                if (categoryParent == null)
                    throw new BadRequestException(ErrorCode.CATEGORY_NOT_FOUNDED);

                newCategory.ParentCategory = categoryParent;
            }

            await unitOfWork.Category.AddAsync(newCategory);
            await unitOfWork.SaveAsync();

            var newCategoryModel = mapper.Map<CategoryModel>(newCategory);
            return newCategoryModel;
        }

        public async Task<CategoryDetailModel?> GetCategory(Guid id)
        {
            var category = await unitOfWork.Category.FindByIdAsync(id);

            if (category == null)
                throw new NotFoundException(ErrorCode.CATEGORY_NOT_FOUNDED);

            var categoryModel = mapper.Map<CategoryDetailModel>(category);
            return categoryModel;
        }

        public async Task<PagedList<Category, CategoryModel>?> GetList(CategoryParameters parameters)
        {
            var categoryList = unitOfWork.Category.FindAll().Include(x => x.ParentCategory).Include(x => x.Categories).Include(c => c.Products).AsQueryable();

            if (categoryList.Any() && !string.IsNullOrWhiteSpace(parameters.Name))
                categoryList = categoryList.Where(x => x.Name.Contains(parameters.Name));

            ApplySort(ref categoryList, parameters.OrderBy);

            return PagedList<Category, CategoryModel>.ToPageList(categoryList,
                parameters.PageNumber,
                parameters.PageSize,
                mapper);

        }
        private int TotalProducts(Category category)
        {
            var totalProducts = category.Products.Count;
            foreach (var innerCategory in category.Categories)
            {
                totalProducts += TotalProducts(innerCategory);
            }
            return totalProducts;

        }

        private void ApplySort(ref IQueryable<Category> categories, string orderByQueryString)
        {
            if (!categories.Any())
                return;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                categories = categories.OrderByDescending(x => x.UpdatedDate);
                return;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Category).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.CurrentCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                categories = categories.OrderByDescending(x => x.UpdatedDate);
                return;
            }

            categories = categories.OrderBy(orderQuery);
        }

        public async Task<CategoryDetailModel> UpdateCategory(Guid id, UpdateCategoryModel updateCategoryModel)
        {
            var category = await unitOfWork.Category.FindByIdAsync(id);

            if (category == null)
                throw new NotFoundException(ErrorCode.CATEGORY_NOT_FOUNDED);

            var updateCategory = mapper.Map<Category>(updateCategoryModel);

            if (updateCategoryModel.ParentId != null)
            {
                var categoryParent = await unitOfWork.Category.GetById((Guid)updateCategoryModel.ParentId);
                if (categoryParent == null)
                    throw new BadRequestException(ErrorCode.CATEGORY_NOT_FOUNDED);

                category.ParentCategory = categoryParent;
            }

            mapper.Map(updateCategory, category);

            unitOfWork.Category.Update(category);

            await unitOfWork.SaveAsync();

            var categoryModel = mapper.Map<CategoryDetailModel>(category);

            return categoryModel;

        }

        public Task DeleteCategory(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
