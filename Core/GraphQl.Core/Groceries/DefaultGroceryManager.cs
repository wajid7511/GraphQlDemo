using AutoMapper;
using GraphQl.Database;
using GraphQl.Database.Models;
using GraphQlDemo.API.Models;

namespace GraphQl.Core;

public class DefaultGroceryManager : IGroceryManager
{
    private readonly GraphQlDatabaseContext _databaseContext;
    private readonly IMapper _mapper;

    public DefaultGroceryManager(GraphQlDatabaseContext databaseContext, IMapper mapper)
    {
        _databaseContext =
            databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async ValueTask<int> AddGroceryAsync(GroceryInput input)
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));
        try
        {
            var dbAddResult = await _databaseContext.AddAsync(new Grocery() { Name = input.Name });
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
