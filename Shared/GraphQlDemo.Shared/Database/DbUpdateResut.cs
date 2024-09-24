public class DbUpdateResult
{
    public bool IsSuccess { get; private set; }

    public Exception? Exception { get; set; }
    public bool IsError => Exception != null;

    public DbUpdateResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public DbUpdateResult(Exception exception)
    {
        Exception = exception;
        IsSuccess = false;
    }
}
