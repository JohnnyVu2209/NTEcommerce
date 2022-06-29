using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTEcommerce.SharedDataModel;
using NTEcommerce.WebAPI.Constant;
using NTEcommerce.WebAPI.Services.Interface;

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
    }
}
