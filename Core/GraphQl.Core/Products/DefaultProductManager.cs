using AutoMapper;
using GraphQl.Abstractions;
using GraphQl.Database;
using GraphQl.Database.Models;
using GraphQlDemo.API.Models;

namespace GraphQl.Core;

public class DefaultProductManager : IProductManager
{
    private readonly GraphQlDatabaseContext _databaseContext;
    private readonly IMapper _mapper;

    public DefaultProductManager(GraphQlDatabaseContext databaseContext, IMapper mapper)
    {
        _databaseContext =
            databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async ValueTask<int> AddProductAsync(ProductInput input)
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));
        try
        {
            var product = _mapper.Map<Product>(input);
            var dbAddResult = await _databaseContext.AddAsync(product);
            await _databaseContext.SaveChangesAsync();
            return dbAddResult.Entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return 0;
        }
    }
}
