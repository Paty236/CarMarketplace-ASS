using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces;
using CarMarketplace.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Enums;

namespace CarMarketplace.Application.Commands.Orders
{
    public class OrderCreateCommand : IRequest<CreateResultDto>
    {
        public OrderCreateCommand(OrderDto model)
        {
            TotalPrice = model.TotalPrice;
            Status = model.Status;
            BuyerId = model.BuyerId;
            Items = model.Items;
        }

        public Guid BuyerId { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, CreateResultDto>
    {
        private readonly IMongoCollection<Order> _order;
        private readonly IMongoCollection<User> _users;

        public OrderCreateCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
            _order = collectionFactory.GetCollection<Order>("Orders");
        }

        public async Task<CreateResultDto> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var buyer = _users.Find(u => u.Id == request.BuyerId).FirstOrDefault();
            var orderItems = request.Items.Select(item => new OrderItem
            {
                CarId = item.Car.Id,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList();

            var create = new Order
            {
                TotalPrice = request.TotalPrice,
                Status = request.Status,
                Buyer = buyer,
                Items = orderItems,
                OrderDate = DateTime.UtcNow
            };

            _order.InsertOne(create);

            return new CreateResultDto
            {
                Success = true,
                Message = "Order was created."
            };
        }
    }
}
