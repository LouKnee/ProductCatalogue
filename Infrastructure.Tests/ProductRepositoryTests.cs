using Domain.Entities;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private ProductCatalogueContext _context = null!;
        private ProductRepository.ProductRepository _repository = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ProductCatalogueContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ProductCatalogueContext();
            _context.Database.EnsureCreated();

            // Seed categories
            _context.Categories.Add(new Category { CategoryId = 1, Name = "Electronics", Description = "Electronic items" });
            _context.Categories.Add(new Category { CategoryId = 2, Name = "Books", Description = "Book items" });
            _context.SaveChanges();

            _repository = new ProductRepository.ProductRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task CreateProductAsync_ShouldCreateProduct()
        {
            var product = await _repository.CreateProductAsync("Laptop", "Gaming Laptop", 1200.99m, 10, "Electronics");

            Assert.That(product, Is.Not.Null);
            Assert.That(product.Name, Is.EqualTo("Laptop"));
            Assert.That(product.Description, Is.EqualTo("Gaming Laptop"));
            Assert.That(product.Price, Is.EqualTo(1200.99m));
            Assert.That(product.StockQuantity, Is.EqualTo(10));
        }

        [Test]
        public void CreateProductAsync_ShouldThrow_WhenCategoryNotFound()
        {
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _repository.CreateProductAsync("Book", "Novel", 19.99m, 5, "NonExistentCategory"));
            Assert.That(ex!.Message, Does.Contain("Category with name 'NonExistentCategory' not found."));
        }

        [Test]
        public void CreateProductAsync_ShouldThrow_WhenNameIsNull()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await _repository.CreateProductAsync(null!, "Desc", 10m, 1, "Electronics"));
            Assert.That(ex!.Message, Does.Contain("Product name cannot be null"));
        }

        [Test]
        public void CreateProductAsync_ShouldThrow_WhenNameIsEmpty()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _repository.CreateProductAsync(string.Empty, "Desc", 10m, 1, "Electronics"));
            Assert.That(ex!.Message, Does.Contain("Product name cannot be empty"));
        }

        [Test]
        public async Task GetProductAsync_ShouldReturnProduct()
        {
            await _repository.CreateProductAsync("Tablet", "Android Tablet", 299.99m, 15, "Electronics");
            var product = await _repository.GetProductAsync("Tablet");

            Assert.That(product, Is.Not.Null);
            Assert.That(product.Name, Is.EqualTo("Tablet"));
        }

        [Test]
        public void GetProductAsync_ShouldThrow_WhenProductNotFound()
        {
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _repository.GetProductAsync("NonExistentProduct"));
            Assert.That(ex!.Message, Does.Contain("Product with name 'NonExistentProduct' not found."));
        }

        [Test]
        public void AssignCategoryAsync_ShouldThrow_WhenProductNotFound()
        {
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _repository.AssignCategoryAsync("NonExistentProduct", "Electronics"));
            Assert.That(ex!.Message, Does.Contain("Product with name 'NonExistentProduct' not found."));
        }

        [Test]
        public async Task AssignCategoryAsync_ShouldThrow_WhenCategoryNotFound()
        {
            await _repository.CreateProductAsync("Book", "Novel", 19.99m, 5, "Books");
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _repository.AssignCategoryAsync("Book", "NonExistentCategory"));
            Assert.That(ex!.Message, Does.Contain("Category with name 'NonExistentCategory' not found."));
        }

        [Test]
        public async Task UpdatePrice_ShouldUpdateProductPrice()
        {
            await _repository.CreateProductAsync("Phone", "Smartphone", 499.99m, 20, "Electronics");
            var updated = await _repository.UpdatePrice("Phone", 599.99m);

            Assert.That(updated.Price, Is.EqualTo(599.99m));
            Assert.That(updated.UpdatedAt, Is.EqualTo(updated.UpdatedAt).Within(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void UpdatePrice_ShouldThrow_WhenProductNotFound()
        {
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _repository.UpdatePrice("NonExistentProduct", 100m));
            Assert.That(ex!.Message, Does.Contain("Product with name 'NonExistentProduct' not found."));
        }

        [Test]
        public async Task UpdateStockQuantity_ShouldUpdateStock()
        {
            await _repository.CreateProductAsync("Camera", "DSLR", 899.99m, 5, "Electronics");
            var updated = await _repository.UpdateStockQuantity("Camera", 12);

            Assert.That(updated.StockQuantity, Is.EqualTo(12));
            Assert.That(updated.UpdatedAt, Is.EqualTo(updated.UpdatedAt).Within(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void UpdateStockQuantity_ShouldThrow_WhenProductNotFound()
        {
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _repository.UpdateStockQuantity("NonExistentProduct", 10));
            Assert.That(ex!.Message, Does.Contain("Product with name 'NonExistentProduct' not found."));
        }
    }
}
