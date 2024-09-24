using System;
using Identity.Models;

namespace Identity.Factory;

public static class ApiResponseFactory
{
    public static ApiResponseModel CreateSuccessResponse(object data)
    {
        return new ApiResponseModel(true, data);
    }
    public static ApiResponseModel CreateErrorResponse(string? message, Exception? exception = null)
    {
        return new ApiResponseModel(false, false, message, exception?.ToString());
    }

}