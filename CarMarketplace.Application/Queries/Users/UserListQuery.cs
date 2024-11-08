using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces;
using CarMarketplace.Application.Interfaces.Pagination;
using CarMarketplace.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Application.Mappings;

namespace CarMarketplace.Application.Queries.Users
{
    public class UserListQuery : IRequest<PaginationResult<UserDto>>
    {
        public UserListQuery(PaginationParameter paginationParameter)
        {
            PaginationParameter = paginationParameter;
        }
        public PaginationParameter PaginationParameter { get; set; }
    }

    public class UserListQueryHandler : IRequestHandler<UserListQuery, PaginationResult<UserDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly IMongoCollection<User> _users;

        public UserListQueryHandler(IPaginationService paginationService, IMongoCollectionFactory collectionFactory)
        {
            _paginationService = paginationService;
            _users = collectionFactory.GetCollection<User>("Users");
        }

        public async Task<PaginationResult<UserDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            var query = request.PaginationParameter;
            var users = _users.AsQueryable();

            return await _paginationService.PaginateAsync(users, query, UserMapping.UserProjection);
        }
    }
}
