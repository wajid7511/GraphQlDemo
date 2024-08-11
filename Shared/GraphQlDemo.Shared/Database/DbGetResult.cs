using System;

namespace GraphQlDemo.Shared.Database;

public class DbGetResult<T>
    where T : class
{
    public bool IsSuccess { get; private set; }

    public T? Data { get; private set; }
    public Exception? Exception { get; set; }
    public bool IsError => Exception != null;

    public DbGetResult(bool isSuccess, T? data = null)
    {
        IsSuccess = isSuccess;
        Data = data;
    }

    public DbGetResult(Exception exception)
    {
        Exception = exception;
        IsSuccess = false;
    }
}
