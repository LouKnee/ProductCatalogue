using Application.DTO;
using MediatR;

namespace Application.Requests
{
    public class GetCategoryRequest : IRequest<CategoryDTO>
    {
        public string? Name { get; set; }
    }
}
