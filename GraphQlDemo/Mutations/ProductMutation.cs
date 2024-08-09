using AutoMapper;
using GraphQl.Abstractions;
using GraphQl.Database;
using GraphQl.Database.Models;
using GraphQlDemo.API.Models;

namespace GraphQlDemo;

[ExtendObjectType<Mutation>]
public class ProductMutation
{
    public async ValueTask<IQueryable<ProductSchema>> AddProduct(
        ProductInput rquest,
        [Service] GraphQlDatabaseContext databaseContext,
        [Service] IMapper mapper,
        [Service] IProductManager productManager
    )
    {
        var id = await productManager.AddProductAsync(rquest);
        if (id > 0)
        {
            var query = databaseContext.Products.Where(p => p.Id == id);
            return mapper.ProjectTo<ProductSchema>(query);
        }
        return new List<ProductSchema>().AsQueryable();
    }
}
