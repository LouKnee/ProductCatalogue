using Application.Commands;
using Application.DTO;
using Application.Interfaces;
using Application.Requests;
using MediatR;

namespace Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, ProductDTO>
    {
        private readonly IProductService _productService;

        public CreateProductHandler(IProductService productService) => _productService = productService;

        public async Task<ProductDTO> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            // Validate request parameters to avoid null reference issues
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Category name cannot be null or empty.", nameof(request.Name));

            // Ensure description is not null
            var description = request.Description ?? string.Empty;
            // Ensure description is not null
            var categoryName = request.CategoryName ?? string.Empty;

            var x = await _productService.CreateProductAsync(request.Name, description, request.Price, request.StockQuantity, categoryName);
            return x;
        }
    }

    public class GetProductHandler : IRequestHandler<GetProductRequest, ProductDTO>
    {
        private readonly IProductService _productService;

        public GetProductHandler(IProductService productService) => _productService = productService;

        public async Task<ProductDTO> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Product name cannot be null or empty.", nameof(request.Name));

            return await _productService.GetProductAsync(request.Name);
        }
    }

    public class AssignCategoryHandler : IRequestHandler<AssignCategoryRequest, ProductDTO>
    {
        private readonly IProductService _productService;

        public AssignCategoryHandler(IProductService productService) => _productService = productService;

        public async Task<ProductDTO> Handle(AssignCategoryRequest request, CancellationToken cancellationToken)
        {
            // Validate request parameters to avoid null reference issues
            if (string.IsNullOrWhiteSpace(request.ProductName))
                throw new ArgumentException("Product name cannot be null or empty.", nameof(request.ProductName));
            if (string.IsNullOrWhiteSpace(request.CategoryName))
                throw new ArgumentException("Category name cannot be null or empty.", nameof(request.CategoryName));

            return await _productService.AssignCategoryAsync(request.ProductName, request.CategoryName);
        }
    }

    public class UpdatePriceHandler : IRequestHandler<UpdatePriceRequest, ProductDTO>
    {
        private readonly IProductService _productService;

        public UpdatePriceHandler(IProductService productService) => _productService = productService;

        public async Task<ProductDTO> Handle(UpdatePriceRequest request, CancellationToken cancellationToken)
        {
            // Validate request parameters to avoid null reference issues
            if (string.IsNullOrWhiteSpace(request.ProductName))
                throw new ArgumentException("Product name cannot be null or empty.", nameof(request.ProductName));

            return await _productService.UpdatePrice(request.ProductName, request.NewPrice);
        }
    }

    public class UpdateStockQuantityHandler : IRequestHandler<UpdateStockQuantityRequest, ProductDTO>
    {
        private readonly IProductService _productService;

        public UpdateStockQuantityHandler(IProductService productService) => _productService = productService;

        public async Task<ProductDTO> Handle(UpdateStockQuantityRequest request, CancellationToken cancellationToken)
        {
            // Validate request parameters to avoid null reference issues
            if (string.IsNullOrWhiteSpace(request.ProductName))
                throw new ArgumentException("Product name cannot be null or empty.", nameof(request.ProductName));

            return await _productService.UpdateStockQuantity(request.ProductName, request.NewStockQuantity);
        }
    }
}
