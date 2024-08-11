using GraphQlDemo.API.Models;

namespace GraphQl.Abstractions;

public interface ICustomerManger
{
    public ValueTask<CustomerDto?> AddCustomerAsync(
        CustomerInput input,
        CancellationToken cancellationToken = default
    );
}
