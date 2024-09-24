using System.Text.Json.Serialization;

namespace Identity.Models;

public class IdentityCreateResponseModel
{
    [JsonPropertyName("graphqlToken")]
    public string GraphqlToken { get; set; } = string.Empty;
    [JsonPropertyName("graphqlTokenExpiryDateTime")]
    public DateTimeOffset GraphqlTokenExpiryDateTime { get; set; } = DateTimeOffset.Now;

    [JsonPropertyName("graphqlRefreshToken")]
    public string GraphqlRefreshToken { get; set; } = string.Empty;
    [JsonPropertyName("graphqlRefreshTokenExpiryDateTime")]
    public DateTimeOffset GraphqlRefreshTokenExpiryDateTime { get; set; } = DateTimeOffset.Now;
}
