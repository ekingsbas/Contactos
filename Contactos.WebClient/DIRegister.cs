using Contactos.WebClient.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Contactos.WebClient
{
    public static class DIRegister
    {
        public static IServiceCollection AddContactsDependencies(this IServiceCollection services)
        {
            InjectServices(services);
            return services;
        }

        private static IServiceCollection InjectServices(IServiceCollection services)
        {
            services.AddSingleton<ContactsClientService>();

            return services;
        }
    }
}
