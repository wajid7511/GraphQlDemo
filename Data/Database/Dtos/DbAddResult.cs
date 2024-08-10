namespace GraphQl.Database;

public class DbAddResult<T>
    where T : class
{
    public bool IsSuccess { get; private set; }

    public T? Entity { get; private set; }
    public Exception? Exception { get; set; }
    public bool IsError => Exception != null;

    public DbAddResult(bool isSuccess, T? entity = null)
    {
        IsSuccess = isSuccess;
        Entity = entity;
    }
}
