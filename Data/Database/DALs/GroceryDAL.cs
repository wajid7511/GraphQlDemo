using GraphQl.Database.Models;
using GraphQlDemo.Shared.Database;

namespace GraphQl.Database.DAL;

public class GroceryDAL : BaseDAL
{
    public GroceryDAL(GraphQlDatabaseContext databaseContext)
        : base(databaseContext) { }

    public virtual async ValueTask<DbAddResult<Grocery>> AddGrocery_Async(Grocery grocery)
    {
        if (grocery == null)
        {
            throw new ArgumentNullException(nameof(grocery));
        }
        return await AddAsync(grocery);
    }
}
