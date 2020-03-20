using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Traning.AspNetCore.EntityFramework.Data.Models;

namespace Traning.AspNetCore.EntityFramework.Data
{
    public interface IShopContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductReview> ProductReviews { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
