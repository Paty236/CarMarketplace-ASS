using CarMarketplace.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Entities;

namespace CarMarketplace.Application.Queries.Users
{
    public class GetRolesQuery : IRequest<List<string>>
    {
    }

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<string>>
    {
        private readonly IMongoCollection<UserRole> _roles;

        public GetRolesQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _roles = collectionFactory.GetCollection<UserRole>("UserRoles");
        }

        public async Task<List<string>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roles
                                .Find(FilterDefinition<UserRole>.Empty)
                                .Project(role => role.Name)
                                .ToListAsync(cancellationToken);

            return roles;
        }
    }
}
