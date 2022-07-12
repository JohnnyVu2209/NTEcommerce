using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NTEcommerce.SharedDataModel;
using NTEcommerce.SharedDataModel.Category;
using NTEcommerce.WebAPI.Exceptions;
using NTEcommerce.WebAPI.Model;
using NTEcommerce.WebAPI.Repository.Interface;
using NTEcommerce.WebAPI.Services.Implement;
using NTEcommerce.WebAPI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NTEcommerce.WebAPI.Constant.MessageCode;

namespace NTEcommerce.WebAPI.Tests
{
    public class CategoryServicesTest
    {
        [Fact]
        public async Task CategoryService_CreateCategory_ReturnCategoryModelResult()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockLogger = new Mock<ILogger<CategoryServices>>();
            var mockMapper = new Mock<IMapper>();

            var categoryModel = new CreateCategoryModel()
            {
                Name = "Quần",
                Description = "đẹp"
            };
            var category = new Category()
            {
                Id = Guid.Parse("edcf73f4-f6c8-4a1b-8376-235a21b1eafb"),
                Name = categoryModel.Name,
                Description = categoryModel.Description
            };
            mockUnitOfWork.Setup(x => x.Category.CheckExistName(categoryModel.Name)).Returns(Task.FromResult(false));

            mockMapper.Setup(x => x.Map<Category>(categoryModel)).Returns(category);

            mockUnitOfWork.Setup(x => x.Category.AddAsync(category)).Returns(Task.FromResult(category));

            mockMapper.Setup(x => x.Map<CategoryModel>(category)).Returns(new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            });

            ICategoryServices categoryServices = new CategoryServices(mockUnitOfWork.Object, mockLogger.Object, mockMapper.Object);

            var result = await categoryServices.CreateCategory(categoryModel);

            Assert.IsType<CategoryModel>(result);
            Assert.Equal("Quần", result.Name);
        }
        [Fact]
        public async Task CategoryService_CreateCategory_ThrowCategoryNameExistException()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockLogger = new Mock<ILogger<CategoryServices>>();

            var mockMapper = new Mock<IMapper>();

            var categoryModel = new CreateCategoryModel()
            {
                Name = "Quần",
                Description = "đẹp"
            };

            mockUnitOfWork.Setup(x => x.Category.CheckExistName(It.IsAny<string>())).Returns(Task.FromResult(true));

            ICategoryServices categoryServices = new CategoryServices(mockUnitOfWork.Object, mockLogger.Object, mockMapper.Object);

            var result = await Assert.ThrowsAsync<BadRequestException>(() => categoryServices.CreateCategory(categoryModel));

            Assert.Equal(ErrorCode.CATEGORY_NAME_ALREADY_EXISTED, result.Message);

        }

        [Fact]
        public async Task CategoryService_CreateCategory_ThrowCategoryParentNotFoundException()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockLogger = new Mock<ILogger<CategoryServices>>();

            var mockMapper = new Mock<IMapper>();

            var categoryModel = new CreateCategoryModel()
            {
                Name = "Quần",
                Description = "đẹp",
                ParentId = Guid.Parse("66e20393-3473-46a6-8f78-74a575661015")
            };

            var category = new Category()
            {
                Id = Guid.Parse("edcf73f4-f6c8-4a1b-8376-235a21b1eafb"),
                Name = categoryModel.Name,
                Description = categoryModel.Description
            };

            mockUnitOfWork.Setup(x => x.Category.CheckExistName(It.IsAny<string>())).Returns(Task.FromResult(false));

            mockMapper.Setup(x => x.Map<Category>(categoryModel)).Returns(category);

            ICategoryServices categoryServices = new CategoryServices(mockUnitOfWork.Object, mockLogger.Object, mockMapper.Object);

            var result = await Assert.ThrowsAsync<BadRequestException>(() => categoryServices.CreateCategory(categoryModel));

            Assert.Equal(ErrorCode.CATEGORY_NOT_FOUNDED, result.Message);

        }

        [Fact]
        public async Task CategoryService_GetCategory_ReturnCategoryDetailModel()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockLogger = new Mock<ILogger<CategoryServices>>();

            var mockMapper = new Mock<IMapper>();

            var categoryModel = new CategoryDetailModel()
            {
                Id = Guid.Parse("edcf73f4-f6c8-4a1b-8376-235a21b1eafb"),
                Name = "Quần",
                Description = "đẹp",
            };

            var category = new Category()
            {
                Id = Guid.Parse("edcf73f4-f6c8-4a1b-8376-235a21b1eafb"),
                Name = categoryModel.Name,
                Description = categoryModel.Description
            };

            mockUnitOfWork.Setup(u => u.Category.FindByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(category));

            mockMapper.Setup(m => m.Map<CategoryDetailModel>(category)).Returns(categoryModel);

            ICategoryServices categoryServices = new CategoryServices(mockUnitOfWork.Object, mockLogger.Object, mockMapper.Object);

            var result = await categoryServices.GetCategory(categoryModel.Id);

            Assert.IsType<CategoryDetailModel>(result);

            Assert.Equal(Guid.Parse("edcf73f4-f6c8-4a1b-8376-235a21b1eafb"), result.Id);

            Assert.Equal("Quần", result.Name);
        }

        [Fact]
        public async Task CategoryService_GetCategory_ThrowCategoryNotFound()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockLogger = new Mock<ILogger<CategoryServices>>();

            var mockMapper = new Mock<IMapper>();

            var id = Guid.Parse("edcf73f4-f6c8-4a1b-8376-235a21b1eafb");

            mockUnitOfWork.Setup(u => u.Category.FindByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult((Category)null));

            ICategoryServices categoryServices = new CategoryServices(mockUnitOfWork.Object, mockLogger.Object, mockMapper.Object);

            var result = await Assert.ThrowsAsync<NotFoundException>(() => categoryServices.GetCategory(id));

            Assert.Equal(ErrorCode.CATEGORY_NOT_FOUNDED, result.Message);
        }

        [Fact]
        public async Task CategoryService_GetList_ReturnPageListCategoryModelResult()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var mockLogger = new Mock<ILogger<CategoryServices>>();

            var mockMapper = new Mock<IMapper>();


        }

    }
}
