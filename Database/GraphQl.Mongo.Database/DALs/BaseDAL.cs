using System.Linq.Expressions;
using GraphQl.Abstractions;
using GraphQl.Mongo.Database.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GraphQl.Mongo.Database.DALs;

public class BaseDAL<T>
    where T : DbBaseModel
{
    private readonly IMongoCollection<T> _collection;

    private readonly IDateTimeProvider _dateTimeProvider;

    public BaseDAL(
        IMongoDatabase database,
        IDateTimeProvider dateTimeProvider,
        string collectionName
    )
    {
        _collection = database.GetCollection<T>(collectionName);
        _dateTimeProvider =
            dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    // Create
    protected async ValueTask<T?> InsertOneAsync(T entity)
    {
        try
        {
            entity.CreatedOn = _dateTimeProvider.UtcNow;
            await _collection.InsertOneAsync(entity);
            return entity; // Return the inserted entity
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

    // Read (by Id)
    protected async ValueTask<T> GetByIdAsync(Guid id)
    {
        return await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
    }

    // Read (all)
    protected async ValueTask<List<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    // Update
    protected async ValueTask UpdateAsync(Guid id, T updatedEntity)
    {
        updatedEntity.LastUpdateTime = _dateTimeProvider.UtcNow;
        await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), updatedEntity);
    }

    // Delete
    protected async ValueTask DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
    }

    // Method to filter based on a predicate
    public async ValueTask<List<T>> FindByPredicateAsync(Expression<Func<T, bool>> predicate)
    {
        var filter = Builders<T>.Filter.Where(predicate);
        return await _collection.Find(filter).ToListAsync();
    }

    // Method to get all IQueryable<Customer> directly from MongoDB
    protected IMongoQueryable<T> GetAllIQueryable()
    {
        return _collection.AsQueryable();
    }
}
