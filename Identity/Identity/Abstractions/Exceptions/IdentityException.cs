namespace Identity.Abstractions.Exceptions;

public class IdentityException : Exception
{
    public bool IsInternal { get; private set; }
    public IdentityException(bool isInternal)
    {
        IsInternal = isInternal;
    }

    public IdentityException(bool isInternal, string? message) : base(message)
    {
        IsInternal = isInternal;
    }

    public IdentityException(bool isInternal, string? message, Exception? innerException) : base(message, innerException)
    {
        IsInternal = isInternal;
    }

}
