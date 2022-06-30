using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTEcommerce.SharedDataModel.Category
{
    public class CatgoryDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryModel CategoryParent { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
