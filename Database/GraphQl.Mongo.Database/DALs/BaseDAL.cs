using GraphQl.Abstractions;
using GraphQl.Mongo.Database.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MongoDB.Driver;

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
    protected async Task<T?> InsertOneAsync(T entity)
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
    protected async Task<T> GetByIdAsync(Guid id)
    {
        return await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
    }

    // Read (all)
    protected async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    // Update
    protected async Task UpdateAsync(Guid id, T updatedEntity)
    {
        updatedEntity.LastUpdateTime = _dateTimeProvider.UtcNow;
        await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), updatedEntity);
    }

    // Delete
    protected async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
    }
}
