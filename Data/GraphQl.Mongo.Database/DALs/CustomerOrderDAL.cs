using GraphQl.Mongo.Database.Models;
using GraphQl.Mongo.Database.Options;
using GraphQlDemo.Shared.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GraphQl.Mongo.Database.DALs;

public class CustomerOrderDAL(
    IMongoDatabase database,
    IDbBaseModelFactory modelFactory,
    IOptions<MongoDbOptions> mongoDbOptions,
    ILogger<CustomerOrderDAL>? logger = null
    ) : BaseDAL<CustomerOrder>(database, modelFactory, mongoDbOptions.Value.OrdersCollectionName)
{
    private readonly ILogger<CustomerOrderDAL>? _logger = logger;

    /// <summary>
    /// Create Customer order 
    /// </summary>
    /// <param name="customerOrder"></param>
    /// <returns></returns>
    public virtual async ValueTask<DbAddResult<CustomerOrder>> CreateCustomerOrderAsync(CustomerOrder customerOrder)
    {
        try
        {
            await InsertOneAsync(customerOrder);
            var isSuccess = Guid.TryParse(customerOrder.Id.ToString(), out Guid resultId);
            return new DbAddResult<CustomerOrder>(isSuccess, customerOrder);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while CreateCustomerOrderAsync");
            return new DbAddResult<CustomerOrder>(false);
        }
    }
    /// <summary>
    /// Get Customer Order by Order Id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async ValueTask<DbGetResult<CustomerOrder>> GetCustomerOrderByIdAsync(Guid id)
    {
        try
        {
            var customerOrder = await base.GetByIdAsync(e => e.Id == id);
            return new DbGetResult<CustomerOrder>(true, customerOrder);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while GetCustomerOrderByIdAsync");
            return new DbGetResult<CustomerOrder>(false);
        }
    }
    public virtual async ValueTask<DbUpdateResult> UpdateCustomerOrder(Guid id, Dictionary<string, object> customerOrder)
    {
        try
        {
            // Create update definitions from the dictionary
            var updateDefinitions = new List<UpdateDefinition<CustomerOrder>>();
            foreach (var kvp in customerOrder)
            {
                // Handle the conversion of the dictionary key to a field path
                // Here, assuming that the dictionary keys directly map to MongoDB field names
                var field = kvp.Key;
                var value = kvp.Value;
                updateDefinitions.Add(Builders<CustomerOrder>.Update.Set(field, value));
            }

            // Combine all update definitions into a single update definition
            var combinedUpdateDefinition = Builders<CustomerOrder>.Update.Combine(updateDefinitions);

            // Update the document with the given id
            await UpdateAsync(c => c.Id == id, combinedUpdateDefinition);

            // Check if the update was successful 
            return new DbUpdateResult(true);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Unable to update Customer Order");
            return new DbUpdateResult(false)
            {
                Exception = ex
            };
        }
    }
}
