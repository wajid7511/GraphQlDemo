using GraphQlDemo.Shared.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphQl.Mongo.Database.Models;

public class CustomerOrder : DbBaseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; } // Reference to the Customer
    public DateTime OrderDate { get; set; }
    public List<CustomerOrderItem> Items { get; set; } = []; // List of products in the order 
    public OrderStatusEnum OrderStatusId { get; set; }
}
