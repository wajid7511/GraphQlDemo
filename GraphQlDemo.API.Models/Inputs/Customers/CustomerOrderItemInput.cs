namespace GraphQlDemo.API.Models;

public class CustomerOrderItemInput
{
    /// <summary>
    /// Reference to the Product Id in SQL
    /// </summary>
    public int Id { get; set; }

    public int Quantity { get; set; }
}
