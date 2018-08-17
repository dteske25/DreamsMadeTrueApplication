using DreamsMadeTrue.Engines;
using DreamsMadeTrue.Engines.Client.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Configuration
    {
        public static IServiceCollection AddEngines(this IServiceCollection services)
        {
            services.AddTransient<IEmailEngine, EmailEngine>();
            services.AddTransient<IUserEngine, UserEngine>();
            return services;
        }
    }
}
