using System;
using GraphQl.Mongo.Database.Models;

namespace GraphQl.Mongo.Database;

public interface IDbBaseModelFactory
{
    T Initialize<T>(T entity, bool isUpdate)
        where T : DbBaseModel;
}
