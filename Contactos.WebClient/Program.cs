using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contactos.WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            
            builder.RootComponents.Add<App>("app");

            var clientName = builder.Configuration["Client:Name"]; 

            var apiBaseUrl = builder.Configuration["ApiServer:BaseAddress"]; 

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
            builder.Services.AddHttpClient(clientName, client => client.BaseAddress = new Uri(apiBaseUrl));

            builder.Services.Configure<JsonSerializerOptions>(options =>
            {
                options.PropertyNameCaseInsensitive = true;
                options.AllowTrailingCommas = true;
            });

            //Inject custom dependencies
            builder.Services.AddContactsDependencies();

            await builder.Build().RunAsync();
        }
    }
}
