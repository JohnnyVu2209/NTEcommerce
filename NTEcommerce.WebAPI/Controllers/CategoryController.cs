﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NTEcommerce.SharedDataModel;
using NTEcommerce.SharedDataModel.Category;
using NTEcommerce.WebAPI.Constant;
using NTEcommerce.WebAPI.Services.Interface;
using static NTEcommerce.WebAPI.Constant.MessageCode;

namespace NTEcommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   /* [Authorize(Roles = "Admin")]*/
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }
        [HttpPost("createCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryModel categoryModel)
        {
            return Ok(await categoryServices.CreateCategory(categoryModel));
        }
        [HttpGet("getList")]
        public async Task<IActionResult> GetListCategory([FromQuery] CategoryParameters parameters)
        {
            var categories = await categoryServices.GetList(parameters);
            var metadata = new
            {
                categories.TotalCount,
                categories.PageSize,
                categories.CurrentPage,
                categories.TotalPages,
                categories.HasNext,
                categories.HasPrevious
            };
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(categories);
        }
        [HttpGet("getCategory/{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            return Ok(await categoryServices.GetCategory(id));
        }

        [HttpPut("updateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryModel updateCategory)
        {
            return Ok(await categoryServices.UpdateCategory(id, updateCategory));
        }
        [HttpDelete("deleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await categoryServices.DeleteCategory(id);
            return Ok(SuccessCode.DELETE_CATEGORY_SUCCESS);
        }
    }
}
