namespace GraphQlDemo.API.Models
{
    public class GrocerySchema
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? LastUpdateTime { get; set; }
    }
}

