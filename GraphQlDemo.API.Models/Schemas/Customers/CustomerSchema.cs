namespace GraphQlDemo.API.Models
{
    public class CustomerSchema
    {
        [GraphQLName("id")]
        public Guid Id { get; set; }

        [GraphQLName("name")]
        public string Name { get; set; } = string.Empty;

        [GraphQLName("email")]
        public string? Email { get; set; }

        [GraphQLName("phoneNumber")]
        public string? PhoneNumber { get; set; }
    }
}
