using AutoMapper;
using GraphQl.Abstractions;
using GraphQlDemo.API.Models;

namespace GraphQlDemo;

[ExtendObjectType<Mutation>]
public class CustomerMutation
{
    public async ValueTask<CustomerSchema?> AddCustomer(
        CustomerInput rquest,
        [Service] ICustomerManger manager,
        [Service] IMapper mapper
    )
    {
        var result = await manager.AddCustomerAsync(rquest);
        return mapper.Map<CustomerSchema>(result);
    }
}
