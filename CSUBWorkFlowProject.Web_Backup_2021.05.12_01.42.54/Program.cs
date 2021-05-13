using CSUBWorkFlowProject.Web.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSUBWorkFlowProject.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<ISearchService, SearchService>();

            builder.Services.AddScoped<IAlertService, AlertService>()
                    .AddScoped<IHttpService, HttpService>()
                    .AddScoped<ILocalStorageService, LocalStorageService>();


            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            var host = builder.Build();

            var accountService = host.Services.GetRequiredService<IAccountService>();
            await accountService.Initialize();

            await host.RunAsync();
        }
    }
}
