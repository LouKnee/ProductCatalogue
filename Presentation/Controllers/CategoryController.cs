using Application.Commands;
using Application.DTO;
using Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/<Category>/retrieve/
        [HttpGet("retrieve/{name}")]
        public async Task<CategoryDTO> Get(string name)
        {
            return await _mediator.Send(new GetCategoryRequest() { Name=name } );
        }

        // POST api/<Category>/create
        [HttpPost]
        public async Task<CategoryDTO> Post([FromBody] CategoryDTO category)
        {
            var newCategory = await _mediator.Send(new CreateCategoryRequest()
            {
                Name = category.Name,
                Description = category.Description
            });

            return newCategory;
        }
    }
}
