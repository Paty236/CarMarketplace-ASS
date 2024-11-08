using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces.Pagination;
using MediatR;

namespace CarMarketplace.Presentation.Services.OrderServices
{
    public interface IOrderService
    {
        Task<PaginationResult<OrderDto>> GetOrders(PaginationParameter queryModel, Guid userId);
        Task<CreateResultDto> OrderCreate(OrderDto request);
        Task<Unit> OrderDelete(Guid id);
        Task<Unit> OrderEdit(OrderDto request);
        Task<OrderDto> GetOrderById(Guid id);
    }
}
