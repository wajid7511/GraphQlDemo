using System;

namespace GraphQl.Abstractions;

public interface IDateTimeProvider
{
    public DateTime Now { get; }

    public DateTime UtcNow { get; }
}
