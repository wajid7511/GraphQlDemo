using System;
using GraphQl.Abstractions;
using GraphQl.Mongo.Database.DALs;
using GraphQl.Mongo.Database.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GraphQl.Mongo.Database;

public class MongoDatabaseRegisterServices : IServiceRegistrationModule
{
    public void RegisterServices(IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection("MongoConfigurations"));

        // Register IMongoDatabase
        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbOptions>>().Value;
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.DatabaseName);
        });
        services.AddScoped<CustomerDAL>();
    }
}
