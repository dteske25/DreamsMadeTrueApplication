using DreamsMadeTrue.Accessors;
using DreamsMadeTrue.Core.Interfaces;
using DreamsMadeTrue.Core.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Configuration
    {
        public static IServiceCollection AddAccessors(this IServiceCollection services, string connectionString, string databaseName = "DreamsMadeTrue")
        {
            var context = new MongoContext(connectionString, databaseName);
            // .AddIndex(...)

            services.AddScoped(s => context);

            services.AddTransient<IEmailAccessor, EmailAccessor>();
            services.AddTransient<IUserAccessor, UserAccessor>();


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddUserStore<UserAccessor>()
                .AddClaimsPrincipalFactory<ApplicationClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static MongoContext AddIndex<T>(this MongoContext context, IndexKeysDefinition<T> indexDefinition, CreateIndexOptions options = null)
        {
            context._database.GetCollection<T>(typeof(T).Name).Indexes.CreateOneAsync(new CreateIndexModel<T>(indexDefinition, options));
            return context;
        }
    }
}
