using System.Linq.Expressions;
using GraphQl.Mongo.Database.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GraphQl.Mongo.Database.DALs;

public class BaseDAL
{
    protected readonly IMongoDatabase _database;
    private readonly IDbBaseModelFactory _modelFactory;

    public BaseDAL(IMongoDatabase database, IDbBaseModelFactory modelFactory)
    {
        _database = database;
        _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
    }

    // Common Insert Method
    protected async Task InsertOneAsync<T>(IMongoCollection<T> collection, T entity)
        where T : class
    {
        try
        {
            if (entity is DbBaseModel dbBaseModel)
            {
                _modelFactory.Initialize(dbBaseModel, false);
            }
            await collection.InsertOneAsync(entity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    // Common Read Method by Id
    protected async Task<T?> GetByIdAsync<T>(IMongoCollection<T> collection, Guid id)
        where T : class
    {
        return await collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
    }

    // Common Read Method (All)
    protected async Task<List<T>> GetAllAsync<T>(IMongoCollection<T> collection)
        where T : class
    {
        return await collection.Find(_ => true).ToListAsync();
    }

    // Common Update Method
    protected async Task UpdateAsync<T>(IMongoCollection<T> collection, Guid id, T updatedEntity)
        where T : class
    {
        if (updatedEntity is DbBaseModel dbBaseModel)
        {
            _modelFactory.Initialize(dbBaseModel, true);
        }
        await collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), updatedEntity);
    }

    // Common Delete Method
    protected async Task DeleteAsync<T>(IMongoCollection<T> collection, Guid id)
        where T : class
    {
        await collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
    }

    // Common Find by Predicate
    protected async Task<List<T>> FindByPredicateAsync<T>(
        IMongoCollection<T> collection,
        Expression<Func<T, bool>> predicate
    )
        where T : class
    {
        var filter = Builders<T>.Filter.Where(predicate);
        return await collection.Find(filter).ToListAsync();
    }

    // Common IQueryable Method
    protected IMongoQueryable<T> GetAllIQueryable<T>(IMongoCollection<T> collection)
        where T : class
    {
        return collection.AsQueryable();
    }
}
