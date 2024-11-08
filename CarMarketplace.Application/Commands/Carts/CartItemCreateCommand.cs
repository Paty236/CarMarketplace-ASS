using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces;
using CarMarketplace.Domain.Entities;
using CarMarketplace.Domain.Enums;
using MediatR;
using MongoDB.Driver;

namespace CarMarketplace.Application.Commands.Carts
{
    public class CartItemCreateCommand : IRequest<Unit>
    {
        public CartItemCreateCommand(ProductDetailsDto model, Guid userId)
        {
            Name = model.Name;
            Description = model.Description;
            Category = model.Category;
            Quantity = model.Quantity;
            Description = model.Description;
            UnitPrice = model.UnitPrice;
            Details = model.Details;
            Image = model.Image;
            UserId = userId;
        }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ProductCategory Category { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Details { get; set; } = null!;
        public byte[]? Image { get; set; } = null!;
        public Guid UserId { get; set; }
    }

    public class CartItemCreateCommandHandler : IRequestHandler<CartItemCreateCommand, Unit>
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<CartItem> _carts;
        private readonly IMongoCollection<ProductDetails> _productDetails;

        public CartItemCreateCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
            _productDetails = collectionFactory.GetCollection<ProductDetails>("ProductDetails");
            _carts = collectionFactory.GetCollection<CartItem>("CartItems");
        }

        public async Task<Unit> Handle(CartItemCreateCommand request, CancellationToken cancellationToken)
        {
            var product = new ProductDetails
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                Details = request.Details,
                Image = request.Image
            };

            var create = new CartItem
            {
                User = _users.Find(u => u.Id == request.UserId).FirstOrDefault(),
                Quantity = request.Quantity,
                ProductId = product.Id,
                Product = product,
                TotalPrice = product.UnitPrice
            };

            _carts.InsertOne(create);

            return Unit.Value;
        }
    }
}
