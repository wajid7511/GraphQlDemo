namespace GraphQlDemo.API.Models
{
    public class GrocerySchema
    {
        [GraphQLName("id")]
        public int Id { get; set; }

        [GraphQLName("name")]
        public string Name { get; set; } = string.Empty;

        [GraphQLName("createdOn")]
        public DateTimeOffset CreatedOn { get; set; }

        [GraphQLName("lastUpdateTime")]
        public DateTimeOffset? LastUpdateTime { get; set; }
    }
}
