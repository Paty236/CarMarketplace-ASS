using CarMarketplace.Application.DTOs;

namespace CarMarketplace.Presentation.Services.TokenServices
{
    public interface IMeService
    {
        Task<ClaimsDto> Me(string jwt);
    }
}
