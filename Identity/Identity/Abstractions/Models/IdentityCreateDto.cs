namespace Identity.Abstractions.Models;

public class IdentityCreateDto
{
    public string Id { get; set; } = string.Empty;
    public string GraphqlToken { get; set; } = string.Empty;
    public string RefreshGraphqlToken { get; set; } = string.Empty;
    public DateTimeOffset GraphqlTokenExpiryDateTime { get; set; } = DateTimeOffset.Now;
}
