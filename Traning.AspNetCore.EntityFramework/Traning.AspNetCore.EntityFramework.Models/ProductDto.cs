using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Traning.AspNetCore.EntityFramework.Logic.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public float Discount { get; set; }
        public float Price { get; set; }
        public float TotalPrice => Price - Price * Discount / 100;
        public IEnumerable<ProductReviewDto> Reviews { get; set; }
    }
}
