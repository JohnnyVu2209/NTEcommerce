using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTEcommerce.SharedDataModel.Product
{
    public class ProductDetailModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public string? Category { get; set; }
        public double? AvgRating { get; set; }
        public List<string>? Images { get; set; }
        public List<ProductReviewModel> Reviews { get; set; }
    }
}
