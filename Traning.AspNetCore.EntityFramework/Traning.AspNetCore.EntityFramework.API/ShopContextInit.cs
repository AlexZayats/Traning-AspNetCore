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
                    Name = "Смартфон Apple iPhone 11 64GB",
                    Description = "Apple iOS, экран 6.1 IPS(828x1792), Apple A13 Bionic, ОЗУ 4 ГБ, флэш - память 64 ГБ, камера 12 Мп, аккумулятор 3046 мАч, 1 SIM",
                    Price = 2300
                });
                context.Products.Add(new Product
                {
                    Name = "Смартфон Samsung Galaxy S10 G973 8GB/128GB",
                    Description = "Android, экран 6.1 AMOLED(1440x3040), Exynos 9820, ОЗУ 8 ГБ, флэш - память 128 ГБ, карты памяти, камера 12 Мп, аккумулятор 3400 мАч, 2 SIM",
                    Price = 1900
                });
                context.SaveChanges();
            }
        }
    }
}
