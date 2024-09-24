using AutoMapper;
using GraphQl.Database;
using GraphQlDemo.API.Models;

namespace GraphQlDemo;

[ExtendObjectType<Query>]
public class GroceryQuery
{
    [UseOffsetPaging(DefaultPageSize = 10, IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<GrocerySchema> GetGroceries(
        [Service] GraphQlDatabaseContext dbContext,
        [Service] IMapper mapper
    )
    {
        return mapper.ProjectTo<GrocerySchema>(dbContext.Groceries);
    }
}
