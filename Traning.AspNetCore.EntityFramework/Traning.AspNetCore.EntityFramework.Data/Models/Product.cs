using System;
using System.Collections.Generic;

namespace Traning.AspNetCore.EntityFramework.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public float Discount { get; set; }
        public float Price { get; set; }
        public DateTime? SaleFrom { get; set; }
        public bool IsDeleted { get; set; }
        public HashSet<ProductReview> Reviews { get; set; }
    }
}
