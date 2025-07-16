using Domain.Entities;

namespace Application.ProductRepository
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(string name, string description, decimal price, int stockQuantity, string categoryName);
        Task<Product> GetProductAsync(string name);
        Task<Product> AssignCategoryAsync(string productName, string categoryName);
        Task<Product> UpdatePrice(string productName, decimal newPrice);
        Task<Product> UpdateStockQuantity(string productName, int newStockQuantity);
    }
}
