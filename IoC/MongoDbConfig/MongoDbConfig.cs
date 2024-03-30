using Application.Interfaces;
using Application.Services;
using Core.Contracts;
using Core.Settings;
using Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
#nullable disable
namespace IoC.MongoDbConfig
{
    public static class MongoDbConfig
    {
        public static IServiceCollection AddMongoConfig(this IServiceCollection services, IConfiguration configuration, string collectionName)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.TryAddSingleton(serviceProvider =>
            {
                ServiceSettings serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                MongoDbSettings mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);

                return mongoClient.GetDatabase(serviceSettings.ServiceName);

            });

            services.TryAddScoped(typeof(IProductService), typeof(ProductService));
            services.AddSingleton<IProductRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();

                return new ProductRepository(database, collectionName);
            });

            return services;

        }
    }
}
