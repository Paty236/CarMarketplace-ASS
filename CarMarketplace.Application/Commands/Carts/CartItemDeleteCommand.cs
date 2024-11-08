using CarMarketplace.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Entities;

namespace CarMarketplace.Application.Commands.Carts
{
    public class CartItemDeleteCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class CartItemDeleteCommandHandler : IRequestHandler<CartItemDeleteCommand, Unit>
    {
        private readonly IMongoCollection<CartItem> _carts;

        public CartItemDeleteCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _carts = collectionFactory.GetCollection<CartItem>("CartItems");
        }

        public async Task<Unit> Handle(CartItemDeleteCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<CartItem>.Filter.Eq(u => u.ProductId, request.Id);
            await _carts.DeleteOneAsync(filter, cancellationToken);

            return Unit.Value;
        }
    }
}
