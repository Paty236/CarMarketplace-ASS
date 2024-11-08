using CarMarketplace.Application.DTOs;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Entities;
using CarMarketplace.Application.Interfaces;
using CarMarketplace.Application.Mappings;

namespace CarMarketplace.Application.Queries.Users
{
    public class UserGetByIdQuery : IRequest<UserDto>
    {
        public UserGetByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, UserDto>
    {
        private readonly IMongoCollection<User> _users;

        public UserGetByIdQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
        }

        public async Task<UserDto> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            var userFilter = Builders<User>.Filter.Eq(r => r.Id, request.Id);
            var user = await _users.Find(userFilter).Project(UserMapping.UserProjection).FirstOrDefaultAsync();
            if (user == null) return null;
            else return user;
        }
    }
}
