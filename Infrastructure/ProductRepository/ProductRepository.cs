using Application.ProductRepository;
using Domain.Entities;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductCatalogueContext? _context;

        public ProductRepository(ProductCatalogueContext context) => _context = context;

        public Task<Product> CreateProductAsync(string name, string description, decimal price, int stockQuantity, string categoryName)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }

            if (name == null)
            {
                throw new ArgumentNullException("Product name cannot be null");
            }

            if (name == string.Empty)
            {
                throw new ArgumentException("Product name cannot be empty");
            }

            var category = _context.Categories.FirstOrDefault(c => c.Name == categoryName) ?? throw new KeyNotFoundException($"Category with name '{categoryName}' not found.");

            var product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = stockQuantity,
                Category = category,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            _context.SaveChanges();
            return Task.FromResult(product);
        }

        public Task<Product> GetProductAsync(string name)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Name == name) ?? throw new KeyNotFoundException($"Product with name '{name}' not found.");
            return Task.FromResult(product);
        }

        public Task<Product> AssignCategoryAsync(string productName, string categoryName)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Name == productName) ?? throw new KeyNotFoundException($"Product with name '{productName}' not found.");
            var category = _context.Categories.FirstOrDefault(c => c.Name == categoryName) ?? throw new KeyNotFoundException($"Category with name '{categoryName}' not found.");
            product.Category = category;
            product.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return Task.FromResult(product);
        }

        public Task<Product> UpdatePrice(string productName, decimal newPrice)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Name == productName) ?? throw new KeyNotFoundException($"Product with name '{productName}' not found.");
            product.Price = newPrice;
            product.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return Task.FromResult(product);
        }

        public Task<Product> UpdateStockQuantity(string productName, int newStockQuantity)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Name == productName) ?? throw new KeyNotFoundException($"Product with name '{productName}' not found.");
            product.StockQuantity = newStockQuantity;
            product.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return Task.FromResult(product);
        }
    }
}
