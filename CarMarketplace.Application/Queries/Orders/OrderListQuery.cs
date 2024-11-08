using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces;
using CarMarketplace.Application.Interfaces.Pagination;
using CarMarketplace.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Application.Mappings;

namespace CarMarketplace.Application.Queries.Orders
{
    public class OrderListQuery : IRequest<PaginationResult<OrderDto>>
    {
        public OrderListQuery(PaginationParameter paginationParameter, Guid userId)
        {
            PaginationParameter = paginationParameter;
            UserId = userId;
        }
        public PaginationParameter PaginationParameter { get; set; }
        public Guid UserId { get; set; }
    }

    public class OrderListQueryHandler : IRequestHandler<OrderListQuery, PaginationResult<OrderDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly IMongoCollection<Order> _orders;

        public OrderListQueryHandler(IPaginationService paginationService, IMongoCollectionFactory collectionFactory)
        {
            _paginationService = paginationService;
            _orders = collectionFactory.GetCollection<Order>("Orders");
        }

        public async Task<PaginationResult<OrderDto>> Handle(OrderListQuery request, CancellationToken cancellationToken)
        {
            var query = request.PaginationParameter;
            var orders = _orders.AsQueryable().Where(item => item.Buyer.Id == request.UserId);

            return await _paginationService.PaginateAsync(orders, query, OrderMapping.OrderProjection);
        }
    }
}
