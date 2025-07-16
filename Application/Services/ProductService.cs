using Application.DTO;
using Application.Interfaces;
using Application.ProductRepository;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<ProductDTO> CreateProductAsync(string name, string description, decimal price, int stockQuantity, string categoryName)
        {
            var product = await _productRepository.CreateProductAsync(name, description, price, stockQuantity, categoryName);
            return new ProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category?.Name,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public async Task<ProductDTO> GetProductAsync(string name)
        {
            var product = await _productRepository.GetProductAsync(name);
            return new ProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category?.Name,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public async Task<ProductDTO> AssignCategoryAsync(string productName, string categoryName)
        {
            var product = await _productRepository.AssignCategoryAsync(productName, categoryName);
            return new ProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category?.Name,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public async Task<ProductDTO> UpdatePrice(string productName, decimal newPrice)
        {
            var product = await _productRepository.UpdatePrice(productName, newPrice);
            return new ProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category?.Name,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public async Task<ProductDTO> UpdateStockQuantity(string productName, int newStockQuantity)
        {
            var product = await _productRepository.UpdateStockQuantity(productName, newStockQuantity);
            return new ProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category?.Name,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}
