using System.Text.Json.Serialization;

namespace Identity.Models;

public class IdentityCreateResponseModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    [JsonPropertyName("graphqlToken")]
    public string GraphqlToken { get; set; } = string.Empty;
    [JsonPropertyName("refreshGraphqlToken")]
    public string RefreshGraphqlToken { get; set; } = string.Empty;
    [JsonPropertyName("graphqlTokenExpiryDateTime")]
    public DateTimeOffset GraphqlTokenExpiryDateTime { get; set; } = DateTimeOffset.Now;


}
