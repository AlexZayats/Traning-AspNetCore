using Microsoft.EntityFrameworkCore;
using System.Linq;
using Traning.AspNetCore.Microservices.Catalog.Domain.Entities;

namespace Traning.AspNetCore.Microservices.Catalog.Application
{
    public interface ICatalogDbContext
    {
        DbSet<Product> Products { get; set; }
    }
}
