using GraphQl.Database.Models;

namespace GraphQl.Database.DAL;

public class GroceryDAL : BaseDAL
{
    public GroceryDAL(GraphQlDatabaseContext databaseContext)
        : base(databaseContext) { }

    public async ValueTask<DbAddResult<Grocery>> AddGrocery_Async(Grocery grocery)
    {
        if (grocery == null)
        {
            throw new ArgumentNullException(nameof(grocery));
        }
        return await AddAsync(grocery);
    }
}
