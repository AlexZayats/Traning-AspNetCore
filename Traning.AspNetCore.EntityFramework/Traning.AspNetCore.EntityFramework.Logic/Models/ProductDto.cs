using System;

namespace Traning.AspNetCore.EntityFramework.Logic.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Discount { get; set; }
        public float Price { get; set; }
        public float TotalPrice => Price - Price * Discount / 100;
    }
}
