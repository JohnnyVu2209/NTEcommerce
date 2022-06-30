﻿using AutoMapper;
using NTEcommerce.SharedDataModel.Category;
using NTEcommerce.WebAPI.Constant;
using NTEcommerce.WebAPI.Exceptions;
using NTEcommerce.WebAPI.Model;
using NTEcommerce.WebAPI.Repository.Interface;
using NTEcommerce.WebAPI.Services.Interface;
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
            try
            {
                if (await unitOfWork.Category.CheckExistName(categoryModel.Name) == true)
                    throw new BadRequestException(ErrorCode.CATEGORY_NAME_ALREADY_EXISTED);

                var newCategory = mapper.Map<Category>(categoryModel);

                if (categoryModel.ParentId != null)
                {
                    var categoryParent = await unitOfWork.Category.GetById((Guid)categoryModel.ParentId);
                    if (categoryParent == null)
                        throw new BadRequestException(ErrorCode.CATEGORY_NOT_EXIST);

                    newCategory.ParentCategory = categoryParent;
                }

                await unitOfWork.Category.AddAsync(newCategory);
                await unitOfWork.SaveAsync();

                var newCategoryModel = mapper.Map<CategoryModel>(newCategory);
                return newCategoryModel;
            }
            catch (Exception e)
            {
                logger.LogError($"***Something went wrong: {e.Message}");
                throw new BadRequestException(ErrorCode.CREATE_CATEGORY_FAILED);
            }
        }

        public async Task<PagedList<Category, CategoryModel>?> GetList(CategoryParameters parameters)
        {
            var categoryList = unitOfWork.Category.FindAll().OrderBy(x => x.Name);
            return PagedList<Category, CategoryModel>.ToPageList(categoryList,
                parameters.PageNumber,
                parameters.PageSize,
                mapper);

        }
    }
}