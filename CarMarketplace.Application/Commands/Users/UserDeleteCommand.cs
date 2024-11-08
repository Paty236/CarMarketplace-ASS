using CarMarketplace.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Entities;

namespace CarMarketplace.Application.Commands.Users
{
    public class UserDeleteCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, Unit>
    {
        private readonly IMongoCollection<User> _users;

        public UserDeleteCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
        }

        public async Task<Unit> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, request.Id);
            await _users.DeleteOneAsync(filter, cancellationToken);

            return Unit.Value;
        }
    }
}
