﻿using CarMarketplace.Application.DTOs;
using CarMarketplace.Application.Interfaces.Pagination;
using MediatR;

namespace CarMarketplace.Presentation.Services.UserAccountServices
{
    public interface IUserAccountService
    {
        Task<PaginationResult<UserDto>> GetUsers(PaginationParameter queryModel);
        Task<CreateResultDto> UserCreate(UserDto request);
        Task<Unit> UserDelete(Guid id);
        Task<Unit> UserEdit(UserDto request);
        Task<UserDto> GetUserById(Guid id);

        Task<List<string>> GetRoles();

        Task<Unit> UserResetPassword(string email, string password);
        Task<bool> SendConfirmationCode(string email);
        Task<bool> VerifyConfirmationCode(string email, string code);
        Task<AuthResultDto> Login(LoginDto request);
        Task<string> Logout();
    }
}
