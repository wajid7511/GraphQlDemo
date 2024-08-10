using System;
using GraphQl.Abstractions;
using GraphQl.Database;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GraphQl.Core.Test;

public class MockFactory
{
    public static GraphQlDatabaseContext GraphQlDatabaseContext()
    {
        GraphQlDatabaseContext _context = null!;
        var options = new DbContextOptionsBuilder<GraphQlDatabaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        Mock<IDateTimeProvider> mockTimeProvider = new();
        _context = new GraphQlDatabaseContext(options, mockTimeProvider.Object);

        // Ensure the database is clean before each test
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        return _context;
    }
}
