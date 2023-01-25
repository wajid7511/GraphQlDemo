using System;
using Database;
using Database.Models;
using HotChocolate;

namespace GraphQlDemo
{
    public class Query
    {
        [UseOffsetPaging(DefaultPageSize = 10, IncludeTotalCount = true, MaxPageSize = 100)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> Products([Service] GraphQlDatabaseContext dbContext)
            => dbContext.Products;
    }
}

