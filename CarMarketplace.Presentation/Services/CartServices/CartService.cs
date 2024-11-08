using CarMarketplace.Application.DTOs;
using MudBlazor;
using System.Net.Http.Json;

namespace CarMarketplace.Presentation.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;

        public CartService(HttpClient httpClient, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
        }

        public async Task<List<CartItemDto>> GetAllItemsAsync(Guid userId)
        {
            try
            {
                var items = await _httpClient.GetFromJsonAsync<List<CartItemDto>>($"api/cart/items/{userId}") ?? new List<CartItemDto>();
                return items;
            }
            catch (Exception ex)
            {
                return new List<CartItemDto>();
            }
        }

        public async Task EmptyCartAsync(Guid userId)
        {
            try
            {
                await _httpClient.DeleteAsync($"api/cart/empty/{userId}");
                _snackbar.Add("Cart emptied successfully.", Severity.Success);
            }
            catch (Exception ex)
            {
                _snackbar.Add("Error emptying cart.", Severity.Error);
            }
        }

        public async Task<bool> AddItemAsync(ProductDetailsDto product, Guid userId)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/cart/add/{userId}", product);
                _snackbar.Add("Added in cart.", Severity.Success);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _snackbar.Add("Error adding item.", Severity.Error);
                return false;
            }
        }

        public async Task RemoveItemAsync(Guid productId)
        {
            try
            {
                await _httpClient.DeleteAsync($"api/cart/remove/{productId}");
                _snackbar.Add("Item removed from cart.", Severity.Success);
            }
            catch (Exception ex)
            {
                _snackbar.Add("Error removing item.", Severity.Error);
            }
        }
    }
}
