using GraphQl.Abstractions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GraphQl.Database.Test;

public class MockFactory
{
    public static GraphQlDatabaseContext GraphQlDatabaseContext(
        Mock<IDateTimeProvider> dateTimeProvider
    )
    {
        GraphQlDatabaseContext _context = null!;
        var options = new DbContextOptionsBuilder<GraphQlDatabaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new GraphQlDatabaseContext(options, dateTimeProvider.Object);

        // Ensure the database is clean before each test
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        return _context;
    }
}
