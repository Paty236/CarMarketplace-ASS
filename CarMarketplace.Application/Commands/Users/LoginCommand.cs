﻿using CarMarketplace.Application.DTOs;
using CarMarketplace.Domain;
using CarMarketplace.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MongoDB.Driver;
using CarMarketplace.Application.Interfaces;

namespace CarMarketplace.Application.Commands.Users
{
    public class LoginCommand : IRequest<AuthResultDto>
    {
        public LoginCommand(LoginDto postModel)
        {
            Email = postModel.Email;
            Password = postModel.Password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string FireBaseTokenId { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResultDto>
    {
        private readonly IMongoCollection<User> _users;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(IMongoCollectionFactory collectionFactory, IConfiguration configuration)
        {
            _users = collectionFactory.GetCollection<User>("Users");
            _configuration = configuration;
        }

        public async Task<AuthResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            AuthResultDto response = new AuthResultDto();

            var appUser = await _users.Find(d => d.Email == request.Email).FirstOrDefaultAsync();

            if (appUser == null)
            {
                response.ResponseMessage = "Credențiale greșite!";
                response.Success = false;
            }
            else
            {
                if (Crypto.VerifyHashedPassword(appUser.PasswordHash, AuthorizationVariables.Salt + request.Password))
                {
                    response.Token = CreateToken(appUser);
                    response.Success = true;
                    response.ResponseMessage = "Login Success";
                }
                else
                {
                    response.ResponseMessage = "Credențiale greșite!";
                    response.Success = false;
                }
            }

            return response;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("role", user.UserRole.Name.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Convert.FromBase64String(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
