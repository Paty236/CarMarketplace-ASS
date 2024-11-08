using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Commands.Carts;
using CarMarketplace.Application.Queries.Carts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketplace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("items/{userId}")]
        public async Task<List<CartItemDto>> GetAllItemsCart([FromRoute] Guid userId)
        {
            return await _mediator.Send(new GetAllItemsCartQuery(userId));
        }

        [HttpPost("add/{userId}")]
        public async Task<Unit> CartItemCreate(ProductDetailsDto request, [FromRoute] Guid userId)
        {
            return await _mediator.Send(new CartItemCreateCommand(request, userId));
        }

        [Authorize]
        [HttpDelete("remove/{id}")]
        public async Task<Unit> CartItemDelete([FromRoute] Guid id)
        {
            return await _mediator.Send(new CartItemDeleteCommand { Id = id });
        }

        [Authorize]
        [HttpDelete("empty/{userId}")]
        public async Task<Unit> EmptyCart([FromRoute] Guid userId)
        {
            return await _mediator.Send(new EmptyCartCommand { UserId = userId });
        }
    }
}
