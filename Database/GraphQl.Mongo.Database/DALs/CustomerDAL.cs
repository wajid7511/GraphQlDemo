using System;
using System.Linq.Expressions;
using GraphQl.Abstractions;
using GraphQl.Mongo.Database.Models;
using GraphQl.Mongo.Database.Options;
using GraphQlDemo.Shared.Database;
using HotChocolate.Execution.Processing;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GraphQl.Mongo.Database.DALs;

public class CustomerDAL : BaseDAL<Customer>
{
    public CustomerDAL(
        IMongoDatabase database,
        IDateTimeProvider dateTimeProvider,
        IOptions<MongoDbOptions> mongoDbOptions
    )
        : base(database, dateTimeProvider, mongoDbOptions.Value.CustomerCollectionName) { }

    // Create
    public async ValueTask<DbAddResult<Customer>> CreateAsync(Customer customer)
    {
        var result = await InsertOneAsync(customer);
        return new DbAddResult<Customer>(result != null, result);
    }

    public IMongoQueryable<Customer> GetCustomersAsync()
    {
        return GetAllIQueryable();
    }
}
