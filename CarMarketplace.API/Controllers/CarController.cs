using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces.Pagination;
using CarMarketplace.Application.Commands.Cars;
using CarMarketplace.Application.Queries.Cars;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketplace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("all")]
        public async Task<PaginationResult<CarDto>> CarGetList(PaginationParameter paginationParameter)
        {
            return await _mediator.Send(new CarListQuery(paginationParameter));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<CarDto> CarGetById([FromRoute] Guid id)
        {
            return await _mediator.Send(new CarGetByIdQuery(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CarCreate(CarDto request)
        {
            var result = await _mediator.Send(new CarCreateCommand(request));
            return Ok(result);
        }

        [Authorize]
        [HttpPut("edit")]
        public async Task<Unit> CarEdit([FromBody] CarDto request)
        {
            return await _mediator.Send(new CarEditCommand(request));
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<Unit> CarDelete([FromRoute] Guid id)
        {
            return await _mediator.Send(new CarDeleteCommand { Id = id });
        }
    }
}
