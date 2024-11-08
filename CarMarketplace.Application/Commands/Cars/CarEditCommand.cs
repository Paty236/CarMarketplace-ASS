using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Entities;
using CarMarketplace.Domain.Enums;

namespace CarMarketplace.Application.Commands.Cars
{
    public class CarEditCommand : IRequest<Unit>
    {
        public CarEditCommand(CarDto model)
        {
            Id = model.Id;
            Make = model.Make;
            Model = model.Model;
            Year = model.Year;
            Price = model.Price;
            Description = model.Description;
            Condition = model.Condition;
            Color = model.Color;
            Mileage = model.Mileage;
            SellerId = model.SellerId;
            Images = model.Images;
        }

        public Guid Id { get; set; }
        public CarMake Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public CarCondition Condition { get; set; }
        public string? Color { get; set; }
        public int Mileage { get; set; }
        public Guid SellerId { get; set; }
        public List<byte[]>? Images { get; set; }
    }

    public class CarEditCommandHandler : IRequestHandler<CarEditCommand, Unit>
    {
        private readonly IMongoCollection<Car> _cars;
        private readonly IMongoCollection<User> _users;

        public CarEditCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
            _cars = collectionFactory.GetCollection<Car>("Cars");
        }

        public async Task<Unit> Handle(CarEditCommand request, CancellationToken cancellationToken)
        {
            var toEdit = await _cars.Find(p => p.Id == request.Id).FirstOrDefaultAsync();
            var user = await _users.Find(p => p.Id == request.Id).FirstOrDefaultAsync();

            if (toEdit != null)
            {
                toEdit.Make = request.Make;
                toEdit.Model = request.Model;
                toEdit.Year = request.Year;
                toEdit.Price = request.Price;
                toEdit.Description = request.Description;
                toEdit.Condition = request.Condition;
                toEdit.Color = request.Color;
                toEdit.Mileage = request.Mileage;
                toEdit.Images = request.Images;
                toEdit.Seller = user;
            }

            await _cars.ReplaceOneAsync(p => p.Id == request.Id, toEdit, cancellationToken: cancellationToken);
            return Unit.Value;
        }
    }
}
