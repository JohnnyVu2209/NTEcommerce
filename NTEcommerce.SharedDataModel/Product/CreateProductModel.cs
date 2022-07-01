using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTEcommerce.SharedDataModel.Product
{
    public class CreateProductModel
    {
        [Required(ErrorMessage ="Product name is required")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage ="Product price is required")]
        public string? Price { get; set; }
        public Guid? CategoryId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
