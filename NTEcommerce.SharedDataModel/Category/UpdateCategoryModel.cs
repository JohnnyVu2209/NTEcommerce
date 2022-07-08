using System.ComponentModel.DataAnnotations;

namespace NTEcommerce.SharedDataModel.Category
{
    public class UpdateCategoryModel
    {
        [Required(ErrorMessage = "Category name is required")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}
