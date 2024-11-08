using CarMarketplace.Application.DTOs;
using CarMarketplace.Domain.Entities;
using System.Linq.Expressions;

namespace CarMarketplace.Application.Mappings
{
    public static class OrderMapping
    {
        public static Expression<Func<Order, OrderDto>> OrderProjection
        {
            get
            {
                return u => new OrderDto
                {
                    Id = u.Id,
                    BuyerId = u.Buyer.Id,
                    TotalPrice = u.TotalPrice,
                    OrderDate = u.OrderDate,
                    Status = u.Status
                };
            }
        }
    }
}
