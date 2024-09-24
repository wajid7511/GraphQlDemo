using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using GraphQl.Database.Models;
using GraphQlDemo.Shared.Database;
using Microsoft.EntityFrameworkCore;

namespace GraphQl.Database.DAL;

public class ProductDAL : BaseDAL
{
    private readonly GraphQlDatabaseContext _databaseContext;

    public ProductDAL(GraphQlDatabaseContext databaseContext)
        : base(databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public virtual async ValueTask<DbAddResult<Product>> AddProduct_Async(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        return await AddAsync(product);
    }

    public virtual async ValueTask<DbGetResult<List<Product>>> GetProducts_Async(
        Expression<Func<Product, bool>> predicate,
        int take = 10,
        int skip = 0,
        bool includeGrocery = false
    )
    {
        ArgumentNullException.ThrowIfNull(predicate);
        try
        {
            var query = _databaseContext.Products.Where(predicate);
            if (includeGrocery)
            {
                query = query.Include(q => q.Grocery);
            }
            query = query.OrderBy(e => e.Id).Skip(skip).Take(take);

            var result = await query.ToListAsync();
            return new DbGetResult<List<Product>>(result.Count != 0, result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new DbGetResult<List<Product>>(false) { Exception = ex };
        }
    }
}
