using GraphQl.Database.Models;

namespace GraphQl.Database.DAL;

public class ProductDAL : BaseDAL
{
    public ProductDAL(GraphQlDatabaseContext databaseContext)
        : base(databaseContext) { }

    public async ValueTask<DbAddResult<Product>> AddProduct_Async(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        return await AddAsync(product);
    }
}
