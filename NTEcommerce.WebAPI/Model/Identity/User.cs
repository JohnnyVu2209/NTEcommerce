﻿using Microsoft.AspNetCore.Identity;

namespace NTEcommerce.WebAPI.Model.Identity
{
    public class User: IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
