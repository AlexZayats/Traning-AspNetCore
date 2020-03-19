using System.Linq;
using Traning.AspNetCore.EntityFramework.Data.Models;

namespace Traning.AspNetCore.EntityFramework.API
{
    public static class ShopContextInit
    {
        public static void Init(ShopContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.Add(new Product 
                { 
                    Name = "Cool Product",
                    Description = "Cool product!",
                    Price = 1000
                });
                context.SaveChanges();
            }
        }
    }
}
