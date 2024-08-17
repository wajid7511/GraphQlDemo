using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphQl.Mongo.Database.Models;

public class Customer : DbBaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonRequired]
    [BsonRepresentation(BsonType.String)]
    public string Name { get; set; } = string.Empty;

    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
