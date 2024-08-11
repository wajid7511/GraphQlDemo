namespace GraphQlDemo.API.Models;

public class CustomerOrderInput 
{ 
    public Guid CustomerId { get; set; } // Reference to the Customer 
    public List<CustomerOrderItemInput> Products { get; set; } = []; // List of product ids in the order
 }
