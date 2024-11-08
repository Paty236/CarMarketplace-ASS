using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces;
using CarMarketplace.Domain.Entities;
using CarMarketplace.Domain;
using MediatR;
using MongoDB.Driver;

namespace CarMarketplace.Application.Commands.Users
{
    public class UserCreateCommand : IRequest<CreateResultDto>
    {
        public UserCreateCommand(UserDto createUser)
        {
            Email = createUser.Email;
            FirstName = createUser.FirstName;
            LastName = createUser.LastName;
            PhoneNumber = createUser.PhoneNumber;
            Password = createUser.Password;
            UserRole = createUser.Role;
        }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? UserRole { get; set; }
    }

    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, CreateResultDto>
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<UserRole> _roles;

        public UserCreateCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
            _roles = collectionFactory.GetCollection<UserRole>("UserRoles");
        }

        public async Task<CreateResultDto> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var existingUser = _users.Find(u => u.Email == request.Email).FirstOrDefault();
            if (existingUser != null)
            {
                return new CreateResultDto
                {
                    Success = false,
                    Message = $"User with email {request.Email} exists."
                };
            }

            var role = await _roles.Find(r => r.Name == request.UserRole).FirstOrDefaultAsync();
            if (role == null) role = await _roles.Find(r => r.Name == "User").FirstOrDefaultAsync();
            var createUser = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserRole = role,
                SecurityCode = "",
                CreatedAt = DateTime.UtcNow,
                PasswordHash = Crypto.HashPassword(AuthorizationVariables.Salt + request.Password)
            };

            _users.InsertOne(createUser);

            return new CreateResultDto
            {
                Success = true,
                Message = "User created."
            };
        }
    }
}
