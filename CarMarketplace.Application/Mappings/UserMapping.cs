using CarMarketplace.Application.DTOs;
using CarMarketplace.Domain.Entities;
using System.Linq.Expressions;

namespace CarMarketplace.Application.Mappings
{
    public static class UserMapping
    {
        public static Expression<Func<User, UserDto>> UserProjection
        {
            get
            {
                return u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = u.UserRole.Name,
                    UserRoleId = u.UserRole.Id,
                    CreatedAt = u.CreatedAt
                };
            }
        }
    }
}
