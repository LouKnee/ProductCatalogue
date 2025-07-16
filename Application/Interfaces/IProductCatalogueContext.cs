using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IProductCatalogueContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
    }
}
