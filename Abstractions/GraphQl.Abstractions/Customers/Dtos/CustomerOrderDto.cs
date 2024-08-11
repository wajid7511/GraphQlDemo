using System;

namespace GraphQl.Abstractions.Customers.Dtos;

public class CustomerOrderDto
{
    public Guid Id { get; set; } 
    public Guid CustomerId { get; set; } // Reference to the Customer
    public DateTime OrderDate { get; set; }
    public List<CustomerOrderItemDto> Items { get; set; } = []; // List of products in the order
}
