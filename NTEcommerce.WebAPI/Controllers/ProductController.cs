﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NTEcommerce.SharedDataModel.Product;
using NTEcommerce.WebAPI.Constant;
using NTEcommerce.WebAPI.Services.Interface;
using static NTEcommerce.WebAPI.Constant.MessageCode;

namespace NTEcommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices productServices;
        public ProductController(IProductServices productServices)
        {
            this.productServices = productServices;
        }
        [HttpGet("getListProducts")]
        public async Task<IActionResult> GetListProduct([FromQuery]ProductParameters parameters)
        {
            var result = await productServices.GetList(parameters);
            var metadata = new
            {
                result.TotalCount,
                result.PageSize,
                result.CurrentPage,
                result.TotalPages,
                result.HasNext,
                result.HasPrevious
            };
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(result);
        }
        [HttpGet("getProduct/{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var result = await productServices.GetProduct(id);
            return Ok(result);
        }
        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductModel productModel)
        {
            return Ok(await productServices.CreateProduct(productModel));
        }
        [HttpPut("updateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id,[FromForm] UpdateProductModel productModel)
        {
            return Ok(await productServices.UpdateProduct(id,productModel));
        }
        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await productServices.DeleteProduct(id);
            return Ok(SuccessCode.DELETE_PRODUCT_SUCCESS);
        }

        [HttpPost("addReview/{ProductId}")]
        public async Task<IActionResult> AddReview(Guid ProductId,[FromBody] AddProductReviewModel reviewModel)
        {
            return Ok(await productServices.AddReview(ProductId, reviewModel));
        }
    }
}
