using Application.Commands;
using Application.DTO;
using Application.Requests;
using MediatR;
using NSubstitute;
using Presentation.Controllers;

namespace Presentation.Tests
{
    [TestFixture]
    public class CategoryControllerTests
    {
        private IMediator _mediator;
        private CategoryController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new CategoryController(_mediator);
        }

        [Test]
        public async Task Get_ReturnsCategoryDTO_WhenCategoryExists()
        {
            // Arrange
            var categoryName = "Books";
            var expectedCategory = new CategoryDTO { Name = categoryName, Description = "All books" };
            _mediator.Send(Arg.Is<GetCategoryRequest>(r => r.Name == categoryName), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(expectedCategory));

            // Act
            var result = await _controller.Get(categoryName);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(categoryName));
            Assert.That(result.Description, Is.EqualTo("All books"));
        }

        [Test]
        public async Task Post_ReturnsCreatedCategoryDTO()
        {
            // Arrange
            var inputCategory = new CategoryDTO { Name = "Electronics", Description = "Electronic items" };
            var createdCategory = new CategoryDTO { Name = "Electronics", Description = "Electronic items" };
            _mediator.Send(Arg.Is<CreateCategoryRequest>(r => r.Name == inputCategory.Name && r.Description == inputCategory.Description), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(createdCategory));

            // Act
            var result = await _controller.Post(inputCategory);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Electronics"));
            Assert.That(result.Description, Is.EqualTo("Electronic items"));
        }
    }
}
