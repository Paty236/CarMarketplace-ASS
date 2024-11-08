using CarMarketplace.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Entities;

namespace CarMarketplace.Application.Commands.Carts
{
    public class EmptyCartCommand : IRequest<Unit> 
    {
        public Guid UserId { get; set; }
    }

    public class EmptyCartCommandHandler : IRequestHandler<EmptyCartCommand, Unit>
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<CartItem> _carts;

        public EmptyCartCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
            _carts = collectionFactory.GetCollection<CartItem>("CartItems");
        }

        public async Task<Unit> Handle(EmptyCartCommand request, CancellationToken cancellationToken)
        {
            var user = _users.Find(u => u.Id == request.UserId).FirstOrDefault();
            var filter = Builders<CartItem>.Filter.Eq(u => u.User, user);
            await _carts.DeleteManyAsync(filter, cancellationToken);

            return Unit.Value;
        }
    }
}
