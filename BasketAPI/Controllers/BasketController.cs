using BasketAPI.Entities;
using BasketAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{username}", Name = "GetBasket")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username) 
        {
            var basket = await _repository.GetBasket(username);
            return Ok(basket ?? new ShoppingCart(username));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{username}", Name = "DeleteBasket")]
        public async Task<ActionResult<ShoppingCart>> DeleteBasket(string username)
        {
            await _repository.DeleteBasket(username);
            return Ok();
        }
    }
}
