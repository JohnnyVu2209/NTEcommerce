using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NTEcommerce.SharedDataModel;
using NTEcommerce.SharedDataModel.Category;
using NTEcommerce.WebAPI.Model;
using NTEcommerce.WebAPI.Repository.Interface;
using NTEcommerce.WebAPI.Services.Implement;
using NTEcommerce.WebAPI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTEcommerce.WebAPI.Tests
{
    public class CategoryServicesTest
    {
        [Fact]
        public async Task CategoryService_CreateCategory()
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
    }
}
