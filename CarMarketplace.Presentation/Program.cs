using CarMarketplace.Application.DTOs;
using CarMarketplace.Presentation.Pages;
using CarMarketplace.Presentation.Services.CarServices;
using CarMarketplace.Presentation.Services.CartServices;
using CarMarketplace.Presentation.Services.OrderServices;
using CarMarketplace.Presentation.Services.TokenServices;
using CarMarketplace.Presentation.Services.UserAccountServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpClient("CarMarketplace.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped<IMeService, MeService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<CurrentUserService>();
builder.Services.AddSingleton<CurrentUserDto>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CarMarketplace.ServerAPI"));

builder.Services.AddMudServices();

await builder.Build().RunAsync();
