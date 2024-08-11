using GraphQl.Abstractions.Customers.Dtos;
using GraphQlDemo.API.Models;

namespace GraphQl.Abstractions;

public interface ICustomerManager
{
    public ValueTask<CustomerDto?> AddCustomerAsync(
        CustomerInput input,
        CancellationToken cancellationToken = default
    );
    public ValueTask<CustomerOrderDto?> AddCustomerOrderAsync(
        CustomerOrderInput input,
        CancellationToken cancellationToken = default
    );
}
