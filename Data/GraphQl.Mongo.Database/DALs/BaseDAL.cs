using System.Linq.Expressions;
using GraphQl.Mongo.Database.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GraphQl.Mongo.Database.DALs;
public class BaseDAL<T> where T : class
{
    protected readonly IMongoDatabase _database;
    private readonly IDbBaseModelFactory _modelFactory;
    protected readonly IMongoCollection<T> _collection;

    public BaseDAL(IMongoDatabase database, IDbBaseModelFactory modelFactory, string collectionName)
    {
        _database = database;
        _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
        _collection = _database.GetCollection<T>(collectionName);
    }

    // Common Insert Method
    public async Task InsertOneAsync(T entity)
    {
        try
        {
            if (entity is DbBaseModel dbBaseModel)
            {
                _modelFactory.Initialize(dbBaseModel, false);
            }
            await _collection.InsertOneAsync(entity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    // Common Read Method by Id
    public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> filterPredicate)
    {
        return await _collection.Find(filterPredicate).FirstOrDefaultAsync();
    }

    // Common Read Method (All)
    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filterPredicate)
    {
        return await _collection.Find(filterPredicate).ToListAsync();
    }

    // Common Update Method
    public async Task UpdateAsync(Expression<Func<T, bool>> filterPredicate, UpdateDefinition<T> entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        // Create a filter to match the entities based on the predicate
        var filter = Builders<T>.Filter.Where(filterPredicate);

        // Use UpdateManyAsync to update all matching documents
        await _collection.UpdateOneAsync(filter, entity);
    }
    // Common Delete Method
    public async Task<DeleteResult> DeleteAsync(Expression<Func<T, bool>> filterPredicate)
    {
        var filter = Builders<T>.Filter.Where(filterPredicate);
        var result = await _collection.DeleteOneAsync(filter);
        return result;
    }

    // Common Find by Predicate
    public async Task<List<T>> FindByPredicateAsync(Expression<Func<T, bool>> predicate)
    {
        var filter = Builders<T>.Filter.Where(predicate);
        return await _collection.Find(filter).ToListAsync();
    }

    // Common IQueryable Method
    public IMongoQueryable<T> GetAllIQueryable()
    {
        return _collection.AsQueryable();
    }
}
