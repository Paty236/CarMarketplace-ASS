using CarMarketplace.Application.DTOs;

namespace CarMarketplace.Presentation.Services.CartServices
{
    public interface ICartService
    {
        Task<bool> AddItemAsync(ProductDetailsDto product, Guid userId);
        Task RemoveItemAsync(Guid productId);
        Task<List<CartItemDto>> GetAllItemsAsync(Guid userId);
        Task EmptyCartAsync(Guid userId);
    }
}
