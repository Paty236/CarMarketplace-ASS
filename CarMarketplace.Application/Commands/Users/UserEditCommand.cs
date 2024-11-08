using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Entities;

namespace CarMarketplace.Application.Commands.Users
{
    public class UserEditCommand : IRequest<Unit>
    {
        public UserEditCommand(UserDto model)
        {
            Id = model.Id;
            Email = model.Email;
            FirstName = model.FirstName;
            LastName = model.LastName;
            PhoneNumber = model.PhoneNumber;
            UserRole = model.Role;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserRole { get; set; }
    }

    public class UserEditCommandHandler : IRequestHandler<UserEditCommand, Unit>
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<UserRole> _roles;

        public UserEditCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
            _roles = collectionFactory.GetCollection<UserRole>("UserRoles");
        }

        public async Task<Unit> Handle(UserEditCommand request, CancellationToken cancellationToken)
        {
            var userToEdit = await _users.Find(p => p.Id == request.Id).FirstOrDefaultAsync();

            var role = await _roles.Find(r => r.Name == request.UserRole).FirstOrDefaultAsync();
            if (role == null) role = await _roles.Find(r => r.Name == "User").FirstOrDefaultAsync();

            if (userToEdit != null)
            {
                userToEdit.Email = request.Email;
                userToEdit.FirstName = request.FirstName;
                userToEdit.LastName = request.LastName;
                userToEdit.PhoneNumber = request.PhoneNumber;
                userToEdit.UserRole = role;
            }

            await _users.ReplaceOneAsync(p => p.Id == request.Id, userToEdit, cancellationToken: cancellationToken);
            return Unit.Value;
        }
    }
}
