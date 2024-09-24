using AutoMapper;
using GraphQl.Database;
using GraphQlDemo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlDemo;

[ExtendObjectType<Query>]
public class ProductQuery
{
    [UseOffsetPaging(DefaultPageSize = 10, IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductSchema> GetProducts(
        [Service] GraphQlDatabaseContext dbContext,
        [Service] IMapper mapper
    )
    {
        return mapper.ProjectTo<ProductSchema>(dbContext.Products.Include(p => p.Grocery));
    }
}
