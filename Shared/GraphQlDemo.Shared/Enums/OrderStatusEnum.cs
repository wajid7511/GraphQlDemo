namespace GraphQlDemo.Shared.Enums;

public enum OrderStatusEnum
{
    /// <summary>
    /// User just placed the order 
    /// </summary>
    Created = 1,
    /// <summary>
    /// Grocery Recieved the Order and preparing the Order
    /// </summary>
    Processing = 2,
    /// <summary>
    /// Order has been sent for delivery
    /// </summary>
    Placed = 3,
    /// <summary>
    /// Some error occurred
    /// </summary>
    Failed = 4
}
