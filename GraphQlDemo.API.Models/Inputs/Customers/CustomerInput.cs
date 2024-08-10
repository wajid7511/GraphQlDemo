using System;

namespace GraphQlDemo.API.Models;

public class CustomerInput
{
    public string Name { get; set; } = string.Empty;

    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
