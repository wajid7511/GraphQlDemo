using AutoMapper;
using GraphQl.Abstractions;
using GraphQl.Mongo.Database.DALs;
using GraphQl.Mongo.Database.Models;
using GraphQlDemo.API.Models;

namespace GraphQl.Core;

public class DefaultCustomerManger : ICustomerManger
{
    private readonly CustomerDAL _customerDAL;
    private readonly IMapper _mapper;

    public DefaultCustomerManger(CustomerDAL customerDAL, IMapper mapper)
    {
        _customerDAL = customerDAL ?? throw new ArgumentNullException(nameof(customerDAL));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async ValueTask<CustomerDto?> AddCustomerAsync(CustomerInput input)
    {
        ArgumentNullException.ThrowIfNull(input);
        var customer = _mapper.Map<Customer>(input);
        var dbAddResult = await _customerDAL.CreateAsync(customer);
        if (dbAddResult.IsSuccess && dbAddResult.Entity is not null)
        {
            return _mapper.Map<CustomerDto>(dbAddResult.Entity);
        }
        return null;
    }
}
