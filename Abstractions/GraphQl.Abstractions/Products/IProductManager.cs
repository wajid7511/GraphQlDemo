using GraphQlDemo.API.Models;

namespace GraphQl.Abstractions;

public interface IProductManager
{
    public ValueTask<int> AddProductAsync(ProductInput input);
}
