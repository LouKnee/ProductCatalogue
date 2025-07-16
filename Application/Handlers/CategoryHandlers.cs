using Application.Commands;
using Application.DTO;
using Application.Interfaces;
using Application.Requests;
using MediatR;

namespace Application.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, CategoryDTO>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryHandler(ICategoryService categoryService) => _categoryService = categoryService;

        public async Task<CategoryDTO> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            // Validate request parameters to avoid null reference issues
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Category name cannot be null or empty.", nameof(request.Name));

            // Ensure description is not null
            var description = request.Description ?? string.Empty;

            // Create the category
            return await _categoryService.CreateCategoryAsync(request.Name, description);
        }
    }

    public class GetCategoryHandler : IRequestHandler<GetCategoryRequest, CategoryDTO>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryHandler(ICategoryService categoryService) => _categoryService = categoryService;

        public async Task<CategoryDTO> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Category name cannot be null or empty.", nameof(request.Name));

            return await _categoryService.GetCategoryAsync(request.Name);
        }
    }
}
