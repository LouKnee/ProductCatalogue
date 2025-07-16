using Application.DTO;
using MediatR;

namespace Application.Commands
{
    public class CreateCategoryRequest : IRequest<CategoryDTO>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
