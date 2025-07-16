using Application.CategoryRepository;
using Domain.Entities;
using Infrastructure.DBContext;

namespace Infrastructure.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductCatalogueContext? _context;

        public CategoryRepository(ProductCatalogueContext context) => _context = context;

        public Task<Category> CreateCategoryAsync(string name, string desription)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }
            var category = new Category
            {
                Name = name,
                Description = desription
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Task.FromResult(category);
        }

        public Task<Category> GetCategoryAsync(string name)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }
            var category = _context.Categories.FirstOrDefault(c => c.Name == name);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with name '{name}' not found.");
            }
            return Task.FromResult(category);
        }
    }
}
