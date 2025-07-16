using Application.Commands;
using Application.DTO;
using Application.Handlers;
using Application.Interfaces;
using Application.Requests;
using Moq;

namespace Application.Tests
{
    [TestFixture]
    public class CategoryHandlerTests
    {
        private Mock<ICategoryService> _categoryServiceMock;

        [SetUp]
        public void SetUp()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
        }

        [Test]
        public async Task CreateCategoryHandler_Handle_ValidRequest_ReturnsCategoryDTO()
        {
            // Arrange
            var request = new CreateCategoryRequest { Name = "Books", Description = "All books" };
            var expectedCategory = new CategoryDTO { Name = "Books", Description = "All books" };
            _categoryServiceMock
                .Setup(s => s.CreateCategoryAsync(request.Name, request.Description))
                .ReturnsAsync(expectedCategory);

            var handler = new CreateCategoryHandler(_categoryServiceMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(expectedCategory.Name, Is.EqualTo(result.Name));
            Assert.That(expectedCategory.Description, Is.EqualTo(result.Description));
        }

        [Test]
        public void CreateCategoryHandler_Handle_NullOrEmptyName_ThrowsArgumentException()
        {
            var handler = new CreateCategoryHandler(_categoryServiceMock.Object);

            var request = new CreateCategoryRequest { Name = null, Description = "desc" };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.ParamName, Is.EqualTo("Name"));
        }

        [Test]
        public async Task CreateCategoryHandler_Handle_NullDescription_SetsEmptyString()
        {
            var request = new CreateCategoryRequest { Name = "Movies", Description = null };
            var expectedCategory = new CategoryDTO { Name = "Movies", Description = string.Empty };
            _categoryServiceMock
                .Setup(s => s.CreateCategoryAsync(request.Name, string.Empty))
                .ReturnsAsync(expectedCategory);

            var handler = new CreateCategoryHandler(_categoryServiceMock.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(expectedCategory.Description, Is.EqualTo(result.Description));
        }

        [Test]
        public async Task GetCategoryHandler_Handle_ValidRequest_ReturnsCategoryDTO()
        {
            var request = new GetCategoryRequest { Name = "Music" };
            var expectedCategory = new CategoryDTO { Name = "Music", Description = "All music" };
            _categoryServiceMock
                .Setup(s => s.GetCategoryAsync(request.Name))
                .ReturnsAsync(expectedCategory);

            var handler = new GetCategoryHandler(_categoryServiceMock.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(expectedCategory.Name, Is.EqualTo(result.Name));
            Assert.That(expectedCategory.Description, Is.EqualTo(result.Description));
        }

        [Test]
        public void GetCategoryHandler_Handle_NullOrEmptyName_ThrowsArgumentException()
        {
            var handler = new GetCategoryHandler(_categoryServiceMock.Object);

            var request = new GetCategoryRequest { Name = "" };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await handler.Handle(request, CancellationToken.None));
            Assert.That(ex.ParamName, Is.EqualTo("Name"));
        }
    }
}
