using System;
using GraphQl.Abstractions;
using GraphQl.Mongo.Database.Models;

namespace GraphQl.Mongo.Database;

public class DbBaseModelFactory(IDateTimeProvider dateTimeProvider) : IDbBaseModelFactory
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public T Initialize<T>(T entity, bool isUpdate)
        where T : DbBaseModel
    {
        if (entity.CreatedOn == default)
        {
            entity.CreatedOn = _dateTimeProvider.UtcNow;
        }

        if (isUpdate && entity.LastUpdateTime == default)
        {
            entity.LastUpdateTime = _dateTimeProvider.UtcNow;
        }

        return entity;
    }
}
