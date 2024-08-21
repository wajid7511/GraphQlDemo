using GraphQl.Mongo.Database.DALs;
using Identity.Abstractions;
using Identity.Abstractions.Models;

namespace Identity.Core;

public class DefaultIdentityManager(CustomerDAL customerDAL) : IIdentityManager
{
    private readonly CustomerDAL _customerDAL = customerDAL ?? throw new ArgumentNullException(nameof(customerDAL));
    public ValueTask<IdentityCreateDto> CreateIdentityAsync(string customerId)
    {
        return new ValueTask<IdentityCreateDto>();
    }
}
