using GraphQl.Abstractions;
using GraphQlDemo.Shared.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Identity.Core;

public class TokenService(IOptions<JtwTokenOptions> options, IDateTimeProvider dateTimeProvider, ILogger<TokenService>? logger = null)
{
    private readonly JtwTokenOptions _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    private readonly ILogger<TokenService>? _logger = logger;
    public (string, DateTimeOffset) GenerateJwtToken(Guid userId)
    {
        return GetToken(userId, _options.TokenExpirationMinutes);
    }

    public (string, DateTimeOffset) GenerateRefreshToken(Guid userId)
    {
        return GetToken(userId, _options.RefreshTokenExpirationDays * 1440);
    }

    public ClaimsPrincipal? ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.SecretKey);
        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidIssuer = _options.Issuer,
                ValidateIssuer = true,
                ValidAudience = _options.Audience,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error while ValidateJwtToken");
            return null;
        }
    }
    private (string, DateTimeOffset) GetToken(Guid userId, int tokenExpirationMinutes)
    {
        var expiryDateTimeOffset = _dateTimeProvider.UtcNow.AddMinutes(tokenExpirationMinutes);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, JwtClaimConstant.Basic_Role)
            ]),
            Expires = expiryDateTimeOffset,
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (tokenHandler.WriteToken(token), expiryDateTimeOffset);
    }
}
