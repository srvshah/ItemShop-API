using ItemShop.Features.Product.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ItemShop.Features.Product
{

    [Authorize]
    public class ProductsController : ApiController
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
            var id = await _service.Create(model.Name, model.Description, model.ImageUrl, model.Price);

            return Created(nameof(Create), id);

        }

        [HttpGet]
        public async Task<IEnumerable<ProductListingModel>> GetAllProducts()
        {
            return await _service.GetAllProducts();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailModel>> GetProductById(int id)
        {
            var product = await _service.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateModel model)
        {
            var updated = await _service.UpdateProduct(id, model.Name, model.Description, model.ImageUrl, model.Price);
            if (!updated)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _service.DeleteProduct(id);
            if (!deleted)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
