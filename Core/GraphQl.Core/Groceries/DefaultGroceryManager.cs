using AutoMapper;
using GraphQl.Database.DAL;
using GraphQl.Database.Models;
using GraphQlDemo.API.Models;

namespace GraphQl.Core;

public class DefaultGroceryManager : IGroceryManager
{
    private readonly GroceryDAL _groceryDAL;
    private readonly IMapper _mapper;

    public DefaultGroceryManager(GroceryDAL groceryDAL, IMapper mapper)
    {
        _groceryDAL = groceryDAL ?? throw new ArgumentNullException(nameof(groceryDAL));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async ValueTask<int> AddGroceryAsync(GroceryInput input)
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));

        var entity = _mapper.Map<Grocery>(input);

        var dbAddResult = await _groceryDAL.AddGrocery_Async(entity);
        return dbAddResult.Entity?.Id ?? 0;
    }
}
