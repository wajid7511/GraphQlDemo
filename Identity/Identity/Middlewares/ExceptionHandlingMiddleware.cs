using System.Text.Json;
using Identity.Abstractions.Exceptions;
using Identity.Factory;
using Identity.Models;

namespace Identity.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware>? logger = null)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware>? _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            //Continue processing
            if (_next != null)
                await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger?.LogError(ex, "An unhandled exception occurred.");
            if (!context.Response.HasStarted)
            {
                // Handle the exception and return a response
                await HandleExceptionAsync(context, ex);
            }
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        ApiResponseModel apiResponse = new(false);

        if (exception is IdentityException ex && ex.IsInternal)
        {
            //Write some other logic if required 
            apiResponse = ApiResponseFactory.CreateErrorResponse(ex?.Message);
        }
        else
        {
            apiResponse = ApiResponseFactory.CreateErrorResponse(exception?.Message, exception);
        }

        var jsonResponse = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        });
        await context.Response.WriteAsync(jsonResponse);
    }

}
