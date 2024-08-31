using AutoMapper;
using GraphQl.Database.DAL;
using GraphQl.Database.Models;
using GraphQlDemo.API.Models;
using Microsoft.Extensions.Logging;

namespace GraphQl.Core;

public class DefaultGroceryManager : IGroceryManager
{
    private readonly GroceryDAL _groceryDAL;
    private readonly IMapper _mapper;
    private readonly ILogger<DefaultGroceryManager>? _logger = null;

    public DefaultGroceryManager(GroceryDAL groceryDAL, IMapper mapper, ILogger<DefaultGroceryManager>? logger = null)
    {
        _groceryDAL = groceryDAL ?? throw new ArgumentNullException(nameof(groceryDAL));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger;
    }

    public async ValueTask<int> AddGroceryAsync(
        GroceryInput input,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));
        try
        {
            var entity = _mapper.Map<Grocery>(input);

            var dbAddResult = await _groceryDAL.AddGrocery_Async(entity);
            return dbAddResult.Entity?.Id ?? 0;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while add Grocery");
            return 0;
        }
    }
}
