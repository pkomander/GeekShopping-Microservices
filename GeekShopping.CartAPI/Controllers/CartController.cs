using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("find-cart/{id}")]
        //[Authorize]
        public async Task<IActionResult> FindById(string userId)
        {
            var cart = await _cartRepository.FindCartByUserId(userId);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost("add-cart/{id}")]
        //[Authorize]
        public async Task<IActionResult> AddCart(CartVO vo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(vo);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPut("update-cart/{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateCart(CartVO vo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(vo);

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        //[Authorize]
        public async Task<IActionResult> RemoveCart(int id)
        {
            var status = await _cartRepository.RemoveFromCart(id);

            if (!status)
                return BadRequest();

            return Ok(status);
        }
    }
}