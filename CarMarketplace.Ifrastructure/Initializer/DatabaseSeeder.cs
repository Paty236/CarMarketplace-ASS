using MongoDB.Driver;
using CarMarketplace.Domain;
using CarMarketplace.Domain.Entities;
using CarMarketplace.Application.Interfaces;

namespace CarMarketplace.Infrastructure.Initializer
{
    public class DatabaseSeeder
    {
        private readonly IMongoCollectionFactory _collectionFactory;

        public DatabaseSeeder(IMongoCollectionFactory collectionFactory)
        {
            _collectionFactory = collectionFactory;
        }

        public async Task SeedDb()
        {
            await AddUserRole();
            await AddUsers();
        }

        private async Task AddUserRole()
        {
            var rolesCollection = _collectionFactory.GetCollection<UserRole>("UserRoles");

            if (await rolesCollection.CountDocumentsAsync(FilterDefinition<UserRole>.Empty) == 0)
            {
                var adminRole = new UserRole { Id = Guid.NewGuid(), Name = "Admin" };
                var userRole = new UserRole { Id = Guid.NewGuid(), Name = "User" };

                await rolesCollection.InsertManyAsync(new[] { adminRole, userRole });
            }
        }

        private async Task AddUsers()
        {
            string password = "Parola11a#";
            var passwordHash = Crypto.HashPassword(AuthorizationVariables.Salt + password);

            var rolesCollection = _collectionFactory.GetCollection<UserRole>("UserRoles");
            var usersCollection = _collectionFactory.GetCollection<User>("Users");

            if (await usersCollection.CountDocumentsAsync(FilterDefinition<User>.Empty) == 0)
            {
                var adminRole = rolesCollection.Find(r => r.Name == "Admin").FirstOrDefault();
                var userRole = rolesCollection.Find(r => r.Name == "User").FirstOrDefault();

                var adminUser = new User
                {
                    Id = Guid.NewGuid(),
                    PasswordHash = passwordHash,
                    FirstName = "Super",
                    LastName = "Admin",
                    Email = "admin@car.com",
                    UserRole = adminRole,
                    CreatedAt = DateTime.UtcNow
                };

                var masterUser = new User
                {
                    Id = Guid.NewGuid(),
                    PasswordHash = passwordHash,
                    FirstName = "User",
                    LastName = "Client",
                    Email = "user@car.com",
                    UserRole = userRole,
                    CreatedAt = DateTime.UtcNow
                };

                await usersCollection.InsertManyAsync(new[] { adminUser, masterUser });
            }
        }
    }
}