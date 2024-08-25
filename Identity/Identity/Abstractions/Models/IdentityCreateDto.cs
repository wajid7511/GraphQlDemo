namespace Identity.Abstractions.Models;

public class IdentityCreateDto
{
    public string GraphqlToken { get; set; } = string.Empty;
    public DateTimeOffset GraphqlTokenExpiryDateTime { get; set; } = DateTimeOffset.Now;
    public string GraphqlRefreshToken { get; set; } = string.Empty;
    public DateTimeOffset GraphqlRefreshTokenExpiryDateTime { get; set; } = DateTimeOffset.Now;
}
