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
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/<Product>/retrieve/myProduct
        [HttpGet("retrieve/{name}")]
        public async Task<ProductDTO> Get(string name)
        {
            return await _mediator.Send(new GetProductRequest() { Name = name });
        }

        // POST api/<Product>/create
        [HttpPost]
        public async Task<ProductDTO> Post([FromBody] ProductDTO category)
        {
            var newProduct = await _mediator.Send(new CreateProductRequest() 
            {
                Name = category.Name,
                Description = category.Description,
                Price = category.Price,
                StockQuantity = category.StockQuantity,
                CategoryName = category.CategoryName
            });

            return newProduct;
        }

        // PUT api/<Product>/assignCategory/productName/categoryName
        [HttpPut("assignCategory/{productName}/{categoryName}")]
        public async Task<ProductDTO> PutCategory(string productName, string categoryName)
        {
            var updated = await _mediator.Send(new AssignCategoryRequest()
            {
                ProductName = productName,
                CategoryName = categoryName
            });

            return updated;
        }

        // PUT api/<Product>/updateStock/name/stockQuantity
        [HttpPut("updateStock/{name}/{stockQuantity}")]
        public async Task<ProductDTO> PutStock(string name, int stockQuantity)
        {
            var updated = await _mediator.Send(new UpdateStockQuantityRequest()
            {
                ProductName = name,
                NewStockQuantity = (int)stockQuantity
            });

            return updated;
        }

        // PUT api/<Product>/updatePrice/name/price
        [HttpPut("updatePrice/{name}/{price}")]
        public async Task<ProductDTO> PutPrice(string name, decimal price)
        {
            var updated = await _mediator.Send(new UpdatePriceRequest()
            {
                ProductName = name,
                NewPrice = price
            });

            return updated;
        }
    }
}
