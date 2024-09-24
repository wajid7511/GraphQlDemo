using System;

namespace GraphQlDemo.Shared.Jwt;

public class JtwTokenOptions
{
    public static string CONFIG_PATH = "JwtToken";

    public string SecretKey { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public int TokenExpirationMinutes { get; set; }
    public int RefreshTokenExpirationDays { get; set; }
}
