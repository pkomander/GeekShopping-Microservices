using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Repository;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var products = await _repository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> FindById(long id)
        {
            var product = await _repository.FindById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProductVO vo)
        {
            if (vo == null)
                return BadRequest();

            var product = await _repository.Create(vo);

            return Ok(product);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(ProductVO vo)
        {
            if (vo == null)
                return BadRequest();

            var product = await _repository.Update(vo);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(long id)
        {
            var status = await _repository.Delete(id);

            if (status == false)
                return BadRequest();

            return Ok(status);
        }

    }
}
