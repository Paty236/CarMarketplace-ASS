using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces.Pagination;
using MediatR;
using MudBlazor;
using System.Net.Http.Json;

namespace CarMarketplace.Presentation.Services.CarServices
{
    public class CarService : ICarService
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;

        public CarService(HttpClient httpClient, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
        }

        public async Task<PaginationResult<CarDto>> GetCars(PaginationParameter paginationParameter)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Car/all", paginationParameter);
            if (!result.IsSuccessStatusCode) return default;

            return await result.Content.ReadFromJsonAsync<PaginationResult<CarDto>>();
        }

        public async Task<CreateResultDto> CarCreate(CarDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Car/create", request);
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<CreateResultDto>();
                if (response != null)
                {
                    if (response.Success)
                    {
                        _snackbar.Add(response.Message, Severity.Success);
                        return response;
                    }
                    else
                    {
                        _snackbar.Add(response.Message, Severity.Error);
                        return response;
                    }
                }
                else
                {
                    _snackbar.Add("An error occurred...", Severity.Error);
                    return new CreateResultDto { Success = false, Message = "Răspuns invalid." };
                }
            }
            _snackbar.Add("An error occurred while creating the car...", Severity.Error);
            return new CreateResultDto { Success = false, Message = "Eroare de rețea sau server." };
        }

        public async Task<Unit> CarDelete(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/Car/delete/{id}");
            if (result.IsSuccessStatusCode)
            {
                _snackbar.Add("Car deleted.", Severity.Success);
                return await result.Content.ReadFromJsonAsync<Unit>();
            }
            _snackbar.Add("An error occurred...", Severity.Error);
            return default;
        }

        public async Task<Unit> CarEdit(CarDto request)
        {
            var result = await _httpClient.PutAsJsonAsync("api/Car/edit", request);
            if (result.IsSuccessStatusCode)
            {
                _snackbar.Add("Car edited.", Severity.Success);
                return await result.Content.ReadFromJsonAsync<Unit>();
            }
            _snackbar.Add("An error occurred...", Severity.Error);
            return default;
        }

        public async Task<CarDto> GetCarById(Guid id)
        {
            var result = await _httpClient.GetAsync($"api/Car/{id}");
            return await result.Content.ReadFromJsonAsync<CarDto>();
        }
    }
}
