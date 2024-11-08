using CarMarketplace.Application.DTOs;
using CarMarketplace.Domain.Entities;
using System.Linq.Expressions;

namespace CarMarketplace.Application.Mappings
{
    public static class CarMapping
    {
        public static Expression<Func<Car, CarDto>> CarProjection
        {
            get
            {
                return u => new CarDto
                {
                    Id = u.Id,
                    Make = u.Make,
                    Model = u.Model,
                    Year = u.Year,
                    Price = u.Price,
                    Description = u.Description,
                    Condition = u.Condition,
                    Color = u.Color,
                    Mileage = u.Mileage,
                    SellerId = u.Seller.Id,
                    SellerName = u.Seller.FirstName + " " + u.Seller.LastName,
                    Images = u.Images,
                    CreatedAt = u.CreatedAt
                };
            }
        }
    }
}
