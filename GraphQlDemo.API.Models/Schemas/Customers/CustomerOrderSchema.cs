namespace GraphQlDemo.API.Models;

public class CustomerOrderSchema
 {
    public Guid Id { get; set; } 
    public Guid CustomerId { get; set; } // Reference to the Customer
    public DateTime OrderDate { get; set; }
    public List<CustomerOrderItemSchema> Items { get; set; } = []; // List of products in the order
 }
