﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTEcommerce.SharedDataModel.Category
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public CategoryModel? CategoryParent { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
