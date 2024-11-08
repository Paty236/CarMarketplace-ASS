using CarMarketplace.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Entities;

namespace CarMarketplace.Application.Commands.Orders
{
    public class OrderDeleteCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class OrderDeleteCommandHandler : IRequestHandler<OrderDeleteCommand, Unit>
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderDeleteCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _orders = collectionFactory.GetCollection<Order>("Orders");
        }

        public async Task<Unit> Handle(OrderDeleteCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Order>.Filter.Eq(u => u.Id, request.Id);
            await _orders.DeleteOneAsync(filter, cancellationToken);

            return Unit.Value;
        }
    }
}
