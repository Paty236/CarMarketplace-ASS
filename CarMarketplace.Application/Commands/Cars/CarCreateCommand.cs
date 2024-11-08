using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces;
using CarMarketplace.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Enums;

namespace CarMarketplace.Application.Commands.Cars
{
    public class CarCreateCommand : IRequest<CreateResultDto>
    {
        public CarCreateCommand(CarDto model)
        {
            Make = model.Make;
            Model = model.Model;
            Year = model.Year;
            Price = model.Price;
            Description = model.Description;
            Condition = model.Condition;
            Color = model.Color;
            Mileage = model.Mileage;
            Images = model.Images;
            SellerId = model.SellerId;
        }

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

    public class CarCreateCommandHandler : IRequestHandler<CarCreateCommand, CreateResultDto>
    {
        private readonly IMongoCollection<Car> _cars;
        private readonly IMongoCollection<User> _users;

        public CarCreateCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
            _cars = collectionFactory.GetCollection<Car>("Cars");
        }

        public async Task<CreateResultDto> Handle(CarCreateCommand request, CancellationToken cancellationToken)
        {
            var seller = _users.Find(u => u.Id == request.SellerId).FirstOrDefault();
            var create = new Car
            {
                Make = request.Make,
                Model = request.Model,
                Year = request.Year,
                Price = request.Price,
                Description = request.Description,
                Condition = request.Condition,
                Color = request.Color,
                Mileage = request.Mileage,
                Images = request.Images,
                Seller = seller,
                CreatedAt = DateTime.UtcNow
            };

            _cars.InsertOne(create);

            return new CreateResultDto
            {
                Success = true,
                Message = "Car was created."
            };
        }
    }
}
