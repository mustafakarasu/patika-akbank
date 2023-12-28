using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Veri kaynağında bulunan bütün product'ları getirir.
        /// </summary>
        /// <param name="title">Veri kaynağındaki Title alanından bu değeri içeren productları getirir.</param>
        /// <param name="order">Product'lar Id değerine göre hangi sırada olsun. "asc" artan, "desc" azalan sırada</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public IActionResult GetProducts([FromQuery] string title, string order)
        {
            var products = _productRepository.GetAll(title, order);
            throw new Exception();
            return Ok(products);
        }

        /// <summary>
        /// Product'ın Id değerine göre ürünü getirir.
        /// </summary>
        /// <param name="id">Product'ın Id değeri. int tipinde</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetProductById")]
        public IActionResult GetProductById(int id)
        {
            var product = _productRepository.GetById(id);

            if ( product == null )
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Product eklemek için kullanılan Action.
        /// </summary>
        /// <param name="product">Eklenecek product değeri.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            var addedProduct = _productRepository.Create(product);

            return CreatedAtRoute("GetProductById", new { id = product.Id }, addedProduct);
        }

        /// <summary>
        /// Id değerine göre Product'ı günceller.
        /// </summary>
        /// <param name="id">Product'ın Id değeri.</param>
        /// <param name="product">Güncellenecek product parametresi.</param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct([FromRoute] int id, [FromBody] Product product)
        {
            var isExistsProduct = _productRepository.IsExistsProductById(id);

            if (isExistsProduct == false )
            {
                return NotFound();
            }

            _productRepository.UpdateById(id, product);

            return NoContent();
        }

        /// <summary>
        /// Id değerine göre Product'ı siler.
        /// </summary>
        /// <param name="id">Product'ın Id değeri.</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            var deletedProduct = _productRepository.GetById(id);

            if ( deletedProduct == null )
            {
                return NotFound();
            }

            _productRepository.DeleteById(id);

            return NoContent();
        }

        /// <summary>
        /// Id değerine göre ürünün belli bir kısmını güncelleyen Patch Action'ı.
        /// </summary>
        /// <param name="id">Product'ın Id değeri.</param>
        /// <param name="patchProduct">üncellenecek product parametresi.</param>
        /// <returns></returns>
        [HttpPatch("{id:int}")]
        public IActionResult PatchProduct(int id, [FromBody] JsonPatchDocument<Product> patchProduct)
        {
            if ( patchProduct == null )
            {
                return BadRequest(ModelState);
            }

            var product = _productRepository.GetById(id);

            if ( product == null )
            {
                return NotFound();
            }

            patchProduct.ApplyTo(product, ModelState);

            if ( !ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }
            return new ObjectResult(product);
        }
    }
}
