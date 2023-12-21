using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetProducts([FromQuery] string title, string order)
        {
            var products = DataSource.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(title) )
            {
               products = products.Where(x => x.Title.ToLower().Contains(title.ToLower()));
            }
            if ( !string.IsNullOrWhiteSpace(order) )
            {
                if ( order.ToLower() == "desc" )
                {
                   products = products.OrderByDescending(x => x.Id);
                }
                else
                {
                   products =  products.OrderBy(x => x.Id);
                }
            }

            var result = products.ToList();
            return Ok(result);
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public IActionResult GetProductById(int id)
        {
           var product = DataSource.Products.FirstOrDefault(x => x.Id == id);

           if (product == null)
           {
               return NotFound();
           }

           return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            var addedProduct = DataSource.Create(product);

              return CreatedAtRoute("GetProductById", new {id = product.Id}, addedProduct);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct([FromRoute] int id, [FromBody] Product product)
        {
            var updatedProduct = DataSource.Products.FirstOrDefault(x => x.Id == id);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            updatedProduct.Title = product.Title;   
            updatedProduct.UnitPrice = product.UnitPrice;

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            var deletedProduct = DataSource.Products.FirstOrDefault(x => x.Id == id);

            if (deletedProduct == null )
            {
                return NotFound();
            }

            DataSource.Products.Remove(deletedProduct);
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult PatchProduct(int id, [FromBody] JsonPatchDocument<Product> patchProduct)
        {
            if ( patchProduct == null )
            {
                return BadRequest(ModelState);
            }

            var product = DataSource.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            patchProduct.ApplyTo(product,ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return new ObjectResult(product);
        }
    }
}
