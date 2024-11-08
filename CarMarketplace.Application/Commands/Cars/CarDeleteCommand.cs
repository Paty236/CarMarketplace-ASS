using CarMarketplace.Application.Interfaces;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Domain.Entities;

namespace CarMarketplace.Application.Commands.Cars
{
    public class CarDeleteCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class CarDeleteCommandHandler : IRequestHandler<CarDeleteCommand, Unit>
    {
        private readonly IMongoCollection<Car> _cars;

        public CarDeleteCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _cars = collectionFactory.GetCollection<Car>("Cars");
        }

        public async Task<Unit> Handle(CarDeleteCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Car>.Filter.Eq(u => u.Id, request.Id);
            await _cars.DeleteOneAsync(filter, cancellationToken);

            return Unit.Value;
        }
    }
}
