using AutoMapper;
using GraphQl.Abstractions;
using GraphQl.Database;
using GraphQl.Database.Models;
using GraphQlDemo.API.Models;

namespace GraphQlDemo;

[ExtendObjectType<Mutation>]
public class GroceryMutation
{
    public async ValueTask<IQueryable<GrocerySchema>> AddGrocery(
        GroceryInput rquest,
        [Service] GraphQlDatabaseContext databaseContext,
        [Service] IMapper mapper,
        [Service] IGroceryManager manager
    )
    {
        var id = await manager.AddGroceryAsync(rquest);
        if (id > 0)
        {
            var query = databaseContext.Groceries.Where(g => g.Id == id);
            return mapper.ProjectTo<GrocerySchema>(query);
        }
        return new List<GrocerySchema>().AsQueryable();
    }
}
