using System;

namespace Traning.AspNetCore.EntityFramework.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public float Discount { get; set; }
        public float Price { get; set; }
    }
}
