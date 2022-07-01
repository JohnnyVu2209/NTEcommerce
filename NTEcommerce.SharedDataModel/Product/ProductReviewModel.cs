using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTEcommerce.SharedDataModel.Product
{
    public class ProductReviewModel
    {
        public Guid Id { get; set; }
        public string? Comment { get; set; }
        public int? Rating { get; set; }
        public string Reviewer { get; set; }
    }
}
