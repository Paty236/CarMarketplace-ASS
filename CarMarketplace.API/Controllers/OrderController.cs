using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces.Pagination;
using CarMarketplace.Application.Commands.Orders;
using CarMarketplace.Application.Queries.Orders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketplace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("all/{userId}")]
        public async Task<PaginationResult<OrderDto>> OrderGetList(PaginationParameter paginationParameter, [FromRoute] Guid userId)
        {
            return await _mediator.Send(new OrderListQuery(paginationParameter, userId));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<OrderDto> OrderGetById([FromRoute] Guid id)
        {
            return await _mediator.Send(new OrderGetByIdQuery(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> OrderCreate(OrderDto request)
        {
            var result = await _mediator.Send(new OrderCreateCommand(request));
            return Ok(result);
        }

        [Authorize]
        [HttpPut("edit")]
        public async Task<Unit> OrderEdit([FromBody] OrderDto request)
        {
            return await _mediator.Send(new OrderEditCommand(request));
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<Unit> OrderDelete([FromRoute] Guid id)
        {
            return await _mediator.Send(new OrderDeleteCommand { Id = id });
        }
    }
}
