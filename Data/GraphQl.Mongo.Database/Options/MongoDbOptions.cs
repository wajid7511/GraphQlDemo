using System;

namespace GraphQl.Mongo.Database.Options;

public class MongoDbOptions
{
    public static string CONFIG_PATH = "MongoConfigurations";
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string CustomerCollectionName { get; set; } = string.Empty;
    public string OrdersCollectionName { get; set; } = string.Empty;
}
