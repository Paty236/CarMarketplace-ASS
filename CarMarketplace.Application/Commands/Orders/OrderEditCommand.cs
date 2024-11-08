using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Entities;
using CarMarketplace.Domain.Enums;

namespace CarMarketplace.Application.Commands.Orders
{
    public class OrderEditCommand : IRequest<Unit>
    {
        public OrderEditCommand(OrderDto model)
        {
            Id = model.Id;
            TotalPrice = model.TotalPrice;
            Status = model.Status;
            BuyerId = model.BuyerId;
        }

        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
    }

    public class OrderEditCommandHandler : IRequestHandler<OrderEditCommand, Unit>
    {
        private readonly IMongoCollection<Order> _order;
        private readonly IMongoCollection<User> _users;

        public OrderEditCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
            _order = collectionFactory.GetCollection<Order>("Orders");
        }

        public async Task<Unit> Handle(OrderEditCommand request, CancellationToken cancellationToken)
        {
            var toEdit = await _order.Find(p => p.Id == request.Id).FirstOrDefaultAsync();
            var seller = await _users.Find(p => p.Id == request.Id).FirstOrDefaultAsync();
            var buyer = _users.Find(u => u.Id == request.BuyerId).FirstOrDefault();

            if (toEdit != null)
            {
                toEdit.TotalPrice = request.TotalPrice;
                toEdit.Status = request.Status;
                toEdit.Buyer = buyer;
            }

            await _order.ReplaceOneAsync(p => p.Id == request.Id, toEdit, cancellationToken: cancellationToken);
            return Unit.Value;
        }
    }
}
