using GraphQl.Mongo.Database.Models;
using GraphQl.Mongo.Database.Options;
using GraphQlDemo.Shared.Database;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GraphQl.Mongo.Database.DALs;

public class CustomerDAL(
    IMongoDatabase database, 
    IDbBaseModelFactory modelFactory,
    IOptions<MongoDbOptions> mongoDbOptions
    ) : BaseDAL(database, modelFactory)
{
    private readonly IMongoCollection<Customer> _customerCollection = database.GetCollection<Customer>(mongoDbOptions.Value.CustomerCollectionName);
    private readonly IMongoCollection<CustomerOrder> _orderCollection = database.GetCollection<CustomerOrder>(mongoDbOptions.Value.OrdersCollectionName);

    // Create
    public async ValueTask<DbAddResult<Customer>> CreateAsync(Customer customer)
    {
        await InsertOneAsync(_customerCollection,customer);
        var isSuccess = Guid.TryParse(customer.Id.ToString(), out Guid resultId);
        return new DbAddResult<Customer>(isSuccess, customer);
    }

    public async ValueTask<DbAddResult<CustomerOrder>> CreateOrderAsync(CustomerOrder customer)
    {
        await InsertOneAsync(_orderCollection,customer);
        var isSuccess = Guid.TryParse(customer.Id.ToString(), out Guid resultId);
        return new DbAddResult<CustomerOrder>(isSuccess, customer);
    }

    public IMongoQueryable<Customer> GetCustomersAsync()
    {
        return GetAllIQueryable(_customerCollection);
    }
}
