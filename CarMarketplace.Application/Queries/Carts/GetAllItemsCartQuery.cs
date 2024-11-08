using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces;
using CarMarketplace.Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace CarMarketplace.Application.Queries.Carts
{
    public class GetAllItemsCartQuery : IRequest<List<CartItemDto>>
    {
        public GetAllItemsCartQuery(Guid userId) 
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }

    public class GetAllItemsCartQueryHandler : IRequestHandler<GetAllItemsCartQuery, List<CartItemDto>>
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<CartItem> _carts;

        public GetAllItemsCartQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
            _carts = collectionFactory.GetCollection<CartItem>("CartItems");
        }

        public async Task<List<CartItemDto>> Handle(GetAllItemsCartQuery request, CancellationToken cancellationToken)
        {
            var user = _users.Find(u => u.Id == request.UserId).FirstOrDefault();
            var cartItems = await _carts.Find(item => item.User == user).ToListAsync(cancellationToken);

            var cartItemDtos = cartItems.Select(item => new CartItemDto
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = new ProductDetailsDto
                {
                    Id = item.Product.Id,
                    Name = item.Product.Name,
                    Category = item.Product.Category,
                    Description = item.Product.Description,
                    Quantity = item.Product.Quantity,
                    UnitPrice = item.Product.UnitPrice,
                    Details = item.Product.Details,
                    Image = item.Product.Image
                },
                TotalPrice = item.Product.TotalPrice
            }).ToList();

            return cartItemDtos;
        }
    }
}
