using AutoMapper;
using GraphQl.Abstractions;
using GraphQlDemo.API.Models;

namespace GraphQlDemo;

[ExtendObjectType<Mutation>]
public class CustomerMutation
{
    public async ValueTask<CustomerSchema?> AddCustomer(
        CustomerInput request,
        [Service] ICustomerManager manager,
        [Service] IMapper mapper
    )
    {
        var result = await manager.AddCustomerAsync(request);
        return mapper.Map<CustomerSchema>(result);
    }

    public async ValueTask<CustomerOrderSchema?> AddCustomerOrderAsync(
        CustomerOrderInput rquest,
        [Service] ICustomerManager manager,
        [Service] IMapper mapper
    )
    {
        var result = await manager.AddCustomerOrderAsync(rquest);
        return mapper.Map<CustomerOrderSchema>(result);
    }
}
