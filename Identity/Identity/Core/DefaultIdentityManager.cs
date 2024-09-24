using GraphQl.Mongo.Database.DALs;
using Identity.Abstractions;
using Identity.Abstractions.Models;

namespace Identity.Core;

public class DefaultIdentityManager(CustomerDAL customerDAL, TokenService tokenService) : IIdentityManager
{
    private readonly CustomerDAL _customerDAL = customerDAL ?? throw new ArgumentNullException(nameof(customerDAL));
    private readonly TokenService _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    public async ValueTask<IdentityCreateDto> CreateIdentityAsync(string customerId)
    {
        if (string.IsNullOrEmpty(customerId))
        {
            throw new ArgumentNullException(nameof(customerId));
        }
        var dbGetResult = await _customerDAL.GetCustomerByIdAsync(Guid.Parse(customerId));
        if (dbGetResult.Data != null)
        {
            (string token, DateTimeOffset tokenExpiryDateTimeOffset) = _tokenService.GenerateJwtToken(dbGetResult.Data.Id);
            (string refreshToken, DateTimeOffset refreshTokenExpiryDateTimeOffset) = _tokenService.GenerateRefreshToken(dbGetResult.Data.Id);
            return new IdentityCreateDto()
            {
                GraphqlToken = token,
                GraphqlTokenExpiryDateTime = tokenExpiryDateTimeOffset,
                GraphqlRefreshToken = refreshToken,
                GraphqlRefreshTokenExpiryDateTime = refreshTokenExpiryDateTimeOffset
            };
        }
        throw new Exception("User not found");
    }
}
