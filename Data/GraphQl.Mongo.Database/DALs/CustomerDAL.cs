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
    ) : BaseDAL(database, modelFactory)
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
            await InsertOneAsync(_customerCollection, customer);
            var isSuccess = Guid.TryParse(customer.Id.ToString(), out Guid resultId);
            return new DbAddResult<Customer>(isSuccess, customer);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while CreateCustomerAsync");
            return new DbAddResult<Customer>(false);
        }
    }

    /// <summary>
    /// Create Customer order 
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public virtual async ValueTask<DbAddResult<CustomerOrder>> CreateCustomerOrderAsync(CustomerOrder customer)
    {
        try
        {
            await InsertOneAsync(_orderCollection, customer);
            var isSuccess = Guid.TryParse(customer.Id.ToString(), out Guid resultId);
            return new DbAddResult<CustomerOrder>(isSuccess, customer);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while CreateCustomerOrderAsync");
            return new DbAddResult<CustomerOrder>(false);
        }
    }
    #endregion
    #region Get Functions
    /// <summary>
    /// Get Customer Order by Order Id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async ValueTask<DbGetResult<CustomerOrder>> GetCustomerOrderByIdAsync(Guid id)
    {
        try
        {
            var customerOrder = await base.GetByIdAsync(_orderCollection, id);
            return new DbGetResult<CustomerOrder>(true, customerOrder);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while GetCustomerOrderByIdAsync");
            return new DbGetResult<CustomerOrder>(false);
        }
    }

    public virtual async ValueTask<DbGetResult<Customer>> GetCustomerByIdAsync(Guid id)
    {
        try
        {
            var customer = await base.GetByIdAsync(_customerCollection, id);
            return new DbGetResult<Customer>(true, customer);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while GetCustomerOrderByIdAsync");
            return new DbGetResult<Customer>(false);
        }
    }
    public virtual IMongoQueryable<Customer>? GetCustomersAsync()
    {
        try
        {
            return GetAllIQueryable(_customerCollection);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while GetCustomerOrderByIdAsync");
            return null;
        }
    }
    #endregion

    public virtual async ValueTask<DbUpdateResult<CustomerOrder>> UpdateCustomerOrder(Guid id, CustomerOrder customerOrder)
    {
        try
        {
            await UpdateAsync(_orderCollection, id, customerOrder);
            return new DbUpdateResult<CustomerOrder>(true, customerOrder);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Unable to update Customer Order");
            return new DbUpdateResult<CustomerOrder>(false)
            {
                Exception = ex
            };
        }
    }
}
