using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces.Pagination;
using MediatR;
using MudBlazor;
using System.Net.Http.Json;

namespace CarMarketplace.Presentation.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;

        public OrderService(HttpClient httpClient, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
        }

        public async Task<PaginationResult<OrderDto>> GetOrders(PaginationParameter paginationParameter, Guid userId)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/Order/all/{userId}", paginationParameter);
            if (!result.IsSuccessStatusCode) return default;

            return await result.Content.ReadFromJsonAsync<PaginationResult<OrderDto>>();
        }

        public async Task<CreateResultDto> OrderCreate(OrderDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Order/create", request);
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
            _snackbar.Add("An error occurred while creating the Order...", Severity.Error);
            return new CreateResultDto { Success = false, Message = "Eroare de rețea sau server." };
        }

        public async Task<Unit> OrderDelete(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/Order/delete/{id}");
            if (result.IsSuccessStatusCode)
            {
                _snackbar.Add("Order deleted.", Severity.Success);
                return await result.Content.ReadFromJsonAsync<Unit>();
            }
            _snackbar.Add("An error occurred...", Severity.Error);
            return default;
        }

        public async Task<Unit> OrderEdit(OrderDto request)
        {
            var result = await _httpClient.PutAsJsonAsync("api/Order/edit", request);
            if (result.IsSuccessStatusCode)
            {
                _snackbar.Add("Order edited.", Severity.Success);
                return await result.Content.ReadFromJsonAsync<Unit>();
            }
            _snackbar.Add("An error occurred...", Severity.Error);
            return default;
        }

        public async Task<OrderDto> GetOrderById(Guid id)
        {
            var result = await _httpClient.GetAsync($"api/Order/{id}");
            return await result.Content.ReadFromJsonAsync<OrderDto>();
        }
    }
}
