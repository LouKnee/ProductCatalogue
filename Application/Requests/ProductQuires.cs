using Application.DTO;
using MediatR;

namespace Application.Requests
{
    public class GetProductRequest : IRequest<ProductDTO>
    {
        public string? Name { get; set; }
    }
}
