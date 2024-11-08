using CarMarketplace.Application.DTOs;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Entities;
using CarMarketplace.Application.Interfaces;
using CarMarketplace.Application.Mappings;

namespace CarMarketplace.Application.Queries.Orders
{
    public class OrderGetByIdQuery : IRequest<OrderDto>
    {
        public OrderGetByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class OrderGetByIdQueryHandler : IRequestHandler<OrderGetByIdQuery, OrderDto>
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderGetByIdQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _orders = collectionFactory.GetCollection<Order>("Orders");
        }

        public async Task<OrderDto> Handle(OrderGetByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Order>.Filter.Eq(r => r.Id, request.Id);
            var order = await _orders.Find(filter).Project(OrderMapping.OrderProjection).FirstOrDefaultAsync();
            if (order == null) return null;
            else return order;
        }
    }
}
