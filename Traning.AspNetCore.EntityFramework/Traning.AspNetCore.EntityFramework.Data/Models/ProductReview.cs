using System;

namespace Traning.AspNetCore.EntityFramework.Data.Models
{
    public class ProductReview
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
