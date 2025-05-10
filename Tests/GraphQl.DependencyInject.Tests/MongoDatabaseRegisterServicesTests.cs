using GraphQl.DependencyInject.Tests.Base;
using GraphQl.Mongo.Database;
using GraphQl.Mongo.Database.DALs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace GraphQl.DependencyInject.Tests;

[TestClass]
public class MongoDatabaseRegisterServicesTests : ServiceCollectionAssert
{
    [TestMethod]
    public void RegisterServices_Should_Register_Services_Into_DI()
    {
        // Arrange 
        var module = new MongoDatabaseRegisterServices();

        // Act
        module.RegisterServices(Services, Configuration);

        // Assert
        HasSingleton<IDbBaseModelFactory, DbBaseModelFactory>();
        HasSingleton<IMongoDatabase>();
        HasScoped<CustomerDAL>();
        HasScoped<CustomerOrderDAL>();
    }
}