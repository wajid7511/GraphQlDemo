using AutoMapper;
using GraphQl.Database;
using GraphQlDemo.API.Models;

namespace GraphQlDemo;

[ExtendObjectType<Mutation>]
public class GroceryMutation
{
    public async ValueTask<IQueryable<GrocerySchema>> AddGrocery(
        GroceryInput request,
        [Service] GraphQlDatabaseContext databaseContext,
        [Service] IMapper mapper,
        [Service] IGroceryManager manager
    )
    {
        var id = await manager.AddGroceryAsync(request);
        if (id > 0)
        {
            var query = databaseContext.Groceries.Where(g => g.Id == id);
            return mapper.ProjectTo<GrocerySchema>(query);
        }
        return new List<GrocerySchema>().AsQueryable();
    }
}
