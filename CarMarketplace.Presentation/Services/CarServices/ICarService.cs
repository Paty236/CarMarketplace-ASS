using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces.Pagination;
using MediatR;

namespace CarMarketplace.Presentation.Services.CarServices
{
    public interface ICarService
    {
        Task<PaginationResult<CarDto>> GetCars(PaginationParameter queryModel);
        Task<CreateResultDto> CarCreate(CarDto request);
        Task<Unit> CarDelete(Guid id);
        Task<Unit> CarEdit(CarDto request);
        Task<CarDto> GetCarById(Guid id);
    }
}
