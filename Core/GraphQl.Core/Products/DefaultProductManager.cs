using AutoMapper;
using GraphQl.Abstractions;
using GraphQl.Database.DAL;
using GraphQl.Database.Models;
using GraphQlDemo.API.Models;

namespace GraphQl.Core;

public class DefaultProductManager : IProductManager
{
    private readonly ProductDAL _productDAL;
    private readonly IMapper _mapper;

    public DefaultProductManager(ProductDAL productDAL, IMapper mapper)
    {
        _productDAL = productDAL ?? throw new ArgumentNullException(nameof(productDAL));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async ValueTask<int> AddProductAsync(ProductInput input)
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));
        var product = _mapper.Map<Product>(input);
        var dbAddResult = await _productDAL.AddProduct_Async(product);
        return dbAddResult.Entity?.Id ?? 0;
    }
}
