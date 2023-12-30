using catalog.api.Entities;
using catalog.api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using ThirdParty.BouncyCastle.Asn1;

namespace catalog.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private IProductRepository _repository;
        private ILogger _logger;

        public CatalogController(IProductRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products);

        }
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Products>> GetProductById(string id)
        {
            var product = await _repository.GetProductById(id);
            if (product == null)
            {
                _logger.LogInformation("The product number {id} is not present in the Systme", id);
                return NotFound();
            }
            return Ok(product);

        }
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Products>>> GetProductByCategory(string category)
        {
            var lstProducts = await _repository.GetProductsByCategory(category);
            return Ok(lstProducts);

        }

        [Route("[action]/{name}", Name = "GetProductByName")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Products>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Products>>> GetProductsByName(string name)
        {
            var lstProducts = await _repository.GetProductsByName(name);
            return Ok(lstProducts);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Products>> CreateProduct([FromBody] Products products)
        {
            await _repository.CreateProduct(products);
            return CreatedAtRoute("GetProudct", new { id = products.Id }, products);
        }
        [HttpPut]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProducts([FromBody] Products products)
        {
            return Ok(await _repository.UpdateProduct(products));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Products), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProducts(string id)
        {

            await _repository.DeleteProduct(id);
            return Ok();
        }


    }
}

