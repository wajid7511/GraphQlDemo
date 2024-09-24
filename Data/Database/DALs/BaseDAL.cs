using GraphQlDemo.Shared.Database;

namespace GraphQl.Database.DAL;

public abstract class BaseDAL(GraphQlDatabaseContext databaseContext)
{
    private readonly GraphQlDatabaseContext _databaseContext =
            databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));

    protected async ValueTask<DbAddResult<T>> AddAsync<T>(T entity)
        where T : class
    {
        try
        {
            var dbAddResult = await _databaseContext.AddAsync(entity);
            var result = await _databaseContext.SaveChangesAsync();
            if (result > 0)
            {
                return new DbAddResult<T>(true, dbAddResult.Entity);
            }
            else
            {
                return new DbAddResult<T>(false);
            }
        }
        catch (Exception ex)
        {
            return new DbAddResult<T>(false) { Exception = ex };
        }
    }
}
