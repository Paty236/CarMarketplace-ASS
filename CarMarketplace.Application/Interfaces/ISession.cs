﻿using CarMarketplace.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace CarMarketplace.Application.Interfaces
{
    public interface ISession
    {
        CurrentUserDto CurrentUser { get; set; }
        IHttpContextAccessor HttpContextAccessor { get; set; }
    }
}
