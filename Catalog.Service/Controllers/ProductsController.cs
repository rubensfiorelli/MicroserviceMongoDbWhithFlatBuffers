using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : MainController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) => _productService = productService;

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetId(string productId)
        {
            var product = await _productService.GetId(productId);

            if (productId is null)
            {
                AddErrorToStack("Product not found!");
                return CustomResponse();
            }

            return CustomResponse(product);
        }

        [HttpGet("all")]
        public IActionResult Get()
        {
            var products = _productService.GetAll();

            return products is null ? NotFound() : CustomResponse(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequest request)
        {
            if (await _productService.Add(request))
                return CreatedAtAction(nameof(Create), request);

            return BadRequest(CustomResponse());
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update(string productId, ProductRequest request)
        {

            var existing = await _productService.GetId(productId);
            if (productId is null)
                return NotFound(CustomResponse(existing));

            await _productService.Update(productId, request);

            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(string productId)
        {
            var product = await _productService.GetId(productId);
            if (productId is null)
            {
                AddErrorToStack("Product not found!");
                return CustomResponse(product);

            }

            return NoContent();
        }


    }
}
