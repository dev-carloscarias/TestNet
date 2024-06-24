using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prueba_NET.Application.Commands;
using Prueba_NET.Application.Interfaces;
using Prueba_NET.Application.Queries;
using Prueba_NET.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;


namespace PruebaNET_CarlosCarias_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;
        private readonly IProductStatusService _productStatusService;

        public ProductController(ILogger<ProductController> logger, IMediator mediator, IProductStatusService productStatusService)
        {
            _logger = logger;
            _mediator = mediator;
            _productStatusService = productStatusService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener el producto por ID", Description = "Retorna los datos del producto con el ID especifico")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Fetching product with id {Id}", id);
            var product = await _mediator.Send(new GetProductByIdQuery { Id = id });
            if(product == null)
            {
                _logger.LogWarning("Product with id {Id} not found", id);
                return NotFound();
            }
            var statuses = _productStatusService.GetProductStatuses();
            product.StatusName = statuses.ContainsKey(product.Status) ? statuses[product.Status] : "Unknown";
            return Ok(product);
        }

        [HttpGet("statuses")]
        [SwaggerOperation(Summary = "Obtener los status de los productos", Description = "Retorna los valores de status validos para productos")]
        public IActionResult GetProductStatuses()
        {
            _logger.LogInformation("Fetching statuses ");
            var statuses = _productStatusService.GetProductStatuses();
            return Ok(statuses);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Crear un nuevo producto", Description = "Crea un producto con los datos enviados en el request")]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            _logger.LogInformation("Product with id {Id} created", productId);
            return CreatedAtAction(nameof(Get), new { id = productId }, command);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualizar producto", Description = "Actualiza el producto por id con los datos especificados")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.ProductId)
            {
                _logger.LogWarning("Product id {Id} mismatch", id);
                return BadRequest();
            }
            await _mediator.Send(command);
            _logger.LogInformation("Product with id {Id} updated", id);
            return Ok(command);
        }
    }
}
