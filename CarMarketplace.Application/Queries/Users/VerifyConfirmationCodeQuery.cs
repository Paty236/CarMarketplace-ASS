using CarMarketplace.Application.Interfaces;
using MediatR;
using CarMarketplace.Domain.Entities;
using MongoDB.Driver;

namespace CarMarketplace.Application.Queries.Users
{
    public class VerifyConfirmationCodeQuery : IRequest<bool>
    {
        public VerifyConfirmationCodeQuery(string email, string code)
        {
            Email = email;
            Code = code;
        }

        public string Email { get; set; }
        public string Code { get; set; }
    }

    public class VerifyConfirmationCodeQueryHandler : IRequestHandler<VerifyConfirmationCodeQuery, bool>
    {
        private readonly IMongoCollection<User> _users;

        public VerifyConfirmationCodeQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
        }

        public async Task<bool> Handle(VerifyConfirmationCodeQuery request, CancellationToken cancellationToken)
        {
            var user = _users.Find(p => p.Email == request.Email).FirstOrDefault();
            if (user.SecurityCode == request.Code) return true;
            else return false;
        }
    }
}
