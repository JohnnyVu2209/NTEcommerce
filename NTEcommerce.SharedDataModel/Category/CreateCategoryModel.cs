using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTEcommerce.SharedDataModel.Category
{
    public class CreateCategoryModel
    {
        [Required(ErrorMessage = "Category name is required")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}
