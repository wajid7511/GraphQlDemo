using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GraphQl.Database;

public class GraphQlDatabaseContextFactory : IDesignTimeDbContextFactory<GraphQlDatabaseContext>
{
    public GraphQlDatabaseContext CreateDbContext(string[] args)
    {
        // Configuration for design-time services
        var optionsBuilder = new DbContextOptionsBuilder<GraphQlDatabaseContext>();

        // Use your connection string or configure the options here
        optionsBuilder.UseSqlServer(
            "Server=localhost; Database=GraphQlDemo; User ID=SA;Password=YOURPASSWORD;TrustServerCertificate=true;"
        );

        return new GraphQlDatabaseContext(optionsBuilder.Options);
    }
}
