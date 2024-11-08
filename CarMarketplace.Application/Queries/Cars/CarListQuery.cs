using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces;
using CarMarketplace.Application.Interfaces.Pagination;
using CarMarketplace.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using CarMarketplace.Application.Mappings;

namespace CarMarketplace.Application.Queries.Cars
{
    public class CarListQuery : IRequest<PaginationResult<CarDto>>
    {
        public CarListQuery(PaginationParameter paginationParameter)
        {
            PaginationParameter = paginationParameter;
        }
        public PaginationParameter PaginationParameter { get; set; }
    }

    public class CarListQueryHandler : IRequestHandler<CarListQuery, PaginationResult<CarDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly IMongoCollection<Car> _cars;

        public CarListQueryHandler(IPaginationService paginationService, IMongoCollectionFactory collectionFactory)
        {
            _paginationService = paginationService;
            _cars = collectionFactory.GetCollection<Car>("Cars");
        }

        public async Task<PaginationResult<CarDto>> Handle(CarListQuery request, CancellationToken cancellationToken)
        {
            var query = request.PaginationParameter;
            var cars = _cars.AsQueryable();

            return await _paginationService.PaginateAsync(cars, query, CarMapping.CarProjection);
        }
    }
}
