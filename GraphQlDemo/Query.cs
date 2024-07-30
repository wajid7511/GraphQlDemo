using AutoMapper;
using Database;
using Database.Models;
using GraphQlDemo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlDemo
{
    public class Query
    {
        [UseOffsetPaging(DefaultPageSize = 10, IncludeTotalCount = true, MaxPageSize = 100)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ProductSchema> Products(
            [Service] GraphQlDatabaseContext dbContext,
            [Service] IMapper mapper)
        {
            return mapper.ProjectTo<ProductSchema>(dbContext.Products.Include(p => p.Grocery));
        }
    }
}
