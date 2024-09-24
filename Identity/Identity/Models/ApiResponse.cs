using System.Text.Json.Serialization;

namespace Identity.Models;

public class ApiResponseModel(bool isSuccess, object? data = null, string? message = null, string? exception = null)
{

    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; set; } = isSuccess;
    [JsonPropertyName("data")]
    public object? Data { get; set; } = data;
    [JsonPropertyName("message")]
    public string? Message { get; set; } = message;
    [JsonPropertyName("exception")]
    public string? Exception { get; set; } = exception;
    [JsonPropertyName("isError")]
    public bool IsError => Exception is not null;
}
