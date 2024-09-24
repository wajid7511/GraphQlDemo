using GraphQl.Mongo.Database.Models;
using GraphQl.Mongo.Database.Options;
using GraphQlDemo.Shared.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GraphQl.Mongo.Database.DALs;

public class CustomerDAL(
    IMongoDatabase database,
    IDbBaseModelFactory modelFactory,
    IOptions<MongoDbOptions> mongoDbOptions,
    ILogger<CustomerDAL>? logger = null
    ) : BaseDAL<Customer>(database, modelFactory, mongoDbOptions.Value.CustomerCollectionName)
{
    private readonly IMongoCollection<Customer> _customerCollection = database.GetCollection<Customer>(mongoDbOptions.Value.CustomerCollectionName);
    private readonly IMongoCollection<CustomerOrder> _orderCollection = database.GetCollection<CustomerOrder>(mongoDbOptions.Value.OrdersCollectionName);
    private readonly ILogger<CustomerDAL>? _logger = logger;
    #region Add Functions 
    /// <summary>
    /// Create Customer 
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public virtual async ValueTask<DbAddResult<Customer>> CreateCustomerAsync(Customer customer)
    {
        try
        {
            await InsertOneAsync(customer);
            var isSuccess = Guid.TryParse(customer.Id.ToString(), out Guid resultId);
            return new DbAddResult<Customer>(isSuccess, customer);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while CreateCustomerAsync");
            return new DbAddResult<Customer>(false);
        }
    }
    #endregion

    #region Get Functions

    public virtual async ValueTask<DbGetResult<Customer>> GetCustomerByIdAsync(Guid id)
    {
        try
        {
            var customer = await base.GetByIdAsync(e => e.Id == id);
            return new DbGetResult<Customer>(true, customer);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while GetCustomerByIdAsync");
            return new DbGetResult<Customer>(false);
        }
    }
    public virtual IMongoQueryable<Customer>? GetCustomersAsync()
    {
        try
        {
            return GetAllIQueryable();
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while GetCustomersAsync");
            return null;
        }
    }
    #endregion

}
