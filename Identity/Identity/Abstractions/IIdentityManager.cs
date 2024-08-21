using Identity.Abstractions.Models;

namespace Identity.Abstractions;

public interface IIdentityManager
{
    public ValueTask<IdentityCreateDto> CreateIdentityAsync(string customerId);
}
