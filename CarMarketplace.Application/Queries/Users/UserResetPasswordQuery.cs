using CarMarketplace.Application.Interfaces;
using CarMarketplace.Domain.Entities;
using CarMarketplace.Domain;
using MediatR;
using MongoDB.Driver;

namespace CarMarketplace.Application.Queries.Users
{
    public class UserResetPasswordQuery : IRequest<Unit>
    {
        public UserResetPasswordQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserResetPasswordCommandHandler : IRequestHandler<UserResetPasswordQuery, Unit>
    {
        private readonly IMongoCollection<User> _users;

        public UserResetPasswordCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
        }

        public async Task<Unit> Handle(UserResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var userToEdit = await _users.Find(p => p.Email == request.Email).FirstOrDefaultAsync();
            if (userToEdit != null) userToEdit.PasswordHash = Crypto.HashPassword(AuthorizationVariables.Salt + request.Password);
            await _users.ReplaceOneAsync(p => p.Email == request.Email, userToEdit, cancellationToken: cancellationToken);
            return Unit.Value;
        }
    }
}
