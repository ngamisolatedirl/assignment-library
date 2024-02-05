using LibraryData.Models;
using LibraryData.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var carts = await _cartService.GetAllCarts();
            return Ok(carts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategory(int id)
        {
            var cart = await _cartService.GetCartById(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
        [HttpPost]
        public async Task<IActionResult> PostCart(Cart model)
        {
            var cart = new Cart
            {
                MemberId = model.MemberId,
                BookId = model.BookId,
            };
            await _cartService.AddCart(cart);
            return Ok(cart);
        }
        [HttpPut]
        public async Task<IActionResult> EditCart(int id)
        {
            var cart = await _cartService.GetCartById(id);
            if (cart == null)
            {
                return BadRequest();
            }
            await _cartService.EditCart(cart);
            return Ok(cart);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _cartService.GetCartById(id);
            if (cart == null)
            {
                return BadRequest();
            }
            await _cartService.DeleteCart(id);
            return Ok(cart);
        }
    }
}
