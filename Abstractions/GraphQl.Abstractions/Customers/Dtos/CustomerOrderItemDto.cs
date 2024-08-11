using System;

namespace GraphQl.Abstractions.Customers.Dtos;

public class CustomerOrderItemDto
{
    /// <summary>
    /// Reference to the Product in SQL Database
    /// </summary>
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string GroceryName { get; set; } = string.Empty;

    public int Quantity { get; set; }
    public double Price { get; set; } // Price at the time of the order
}
