using CarMarketplace.Application.DTOs;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Entities;
using CarMarketplace.Application.Interfaces;
using CarMarketplace.Application.Mappings;

namespace CarMarketplace.Application.Queries.Cars
{
    public class CarGetByIdQuery : IRequest<CarDto>
    {
        public CarGetByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class CarGetByIdQueryHandler : IRequestHandler<CarGetByIdQuery, CarDto>
    {
        private readonly IMongoCollection<Car> _cars;

        public CarGetByIdQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _cars = collectionFactory.GetCollection<Car>("Cars");
        }

        public async Task<CarDto> Handle(CarGetByIdQuery request, CancellationToken cancellationToken)
        {
            var carFilter = Builders<Car>.Filter.Eq(r => r.Id, request.Id);
            var car = await _cars.Find(carFilter).Project(CarMapping.CarProjection).FirstOrDefaultAsync();
            if (car == null) return null;
            else return car;
        }
    }
}
