using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Radzen;
using testeBlazor.Models;
using testeBlazor.Services;

namespace testeBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddBlazoredSessionStorage(config =>
                config.JsonSerializerOptions.WriteIndented = true);

            //builder.Services.AddTransient(sp =>
            //    new HttpClient
            //    {
            //        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            //    });
            builder.Services.AddHttpClient<UserService>(client =>
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            await builder.Build().RunAsync();
        }
    }
}
