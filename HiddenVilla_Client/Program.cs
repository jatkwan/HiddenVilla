using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using HiddenVilla_Client.Service;
using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HiddenVilla_Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
            builder.Services.AddScoped<IHotelAmenityService, HotelAmenityService>();
            builder.Services.AddScoped<IRoomOrderDetailService, RoomOrderDetailService>();
            builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            await builder.Build().RunAsync();
        }
    }
}