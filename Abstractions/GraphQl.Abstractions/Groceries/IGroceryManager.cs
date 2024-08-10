using GraphQlDemo.API.Models;

public interface IGroceryManager
{
    public ValueTask<int> AddGroceryAsync(GroceryInput input);
}
