using Application.DTO;
using MediatR;

namespace Application.Commands
{
    public class CreateProductRequest : IRequest<ProductDTO>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? CategoryName { get; set; }
    }

    public class AssignCategoryRequest : IRequest<ProductDTO>
    {
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
    }

    public class UpdatePriceRequest : IRequest<ProductDTO>
    {
        public string? ProductName { get; set; }
        public decimal NewPrice { get; set; }
    }

    public class UpdateStockQuantityRequest : IRequest<ProductDTO>
    {
        public string? ProductName { get; set; }
        public int NewStockQuantity { get; set; }
    }
}
