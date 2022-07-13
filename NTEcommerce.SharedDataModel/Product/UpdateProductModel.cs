using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTEcommerce.SharedDataModel.Product
{
    public class UpdateProductModel
    {
        [Required(ErrorMessage = "Product name is required")]
        public string? Name { get; set; }
        public string? Price { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Product price is required")]
        public Guid? CategoryId { get; set; }
        public List<ProductImageModel> ImageLink { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
