using Application.DTO;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> CreateProductAsync(string name, string description, decimal price, int stockQuantity, string categoryName);
        Task<ProductDTO> GetProductAsync(string name);
        Task<ProductDTO> AssignCategoryAsync(string productName, string categoryName);
        Task<ProductDTO> UpdatePrice(string productName, decimal newPrice);
        Task<ProductDTO> UpdateStockQuantity(string productName, int newStockQuantity);
    }
}
