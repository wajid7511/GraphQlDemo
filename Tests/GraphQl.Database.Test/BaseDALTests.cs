using GraphQl.Abstractions;
using GraphQl.Database.DAL;
using GraphQl.Database.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GraphQl.Database.Test
{
    [TestClass]
    public class BaseDALTests
    {
        private GraphQlDatabaseContext _context = null!;
        private TestBaseDAL _baseDAL = null!;
        private readonly Mock<IDateTimeProvider> _dateTimeProvider = new();

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GraphQlDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new GraphQlDatabaseContext(options, _dateTimeProvider.Object);

            // Ensure the database is clean before each test
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _baseDAL = new TestBaseDAL(_context);
        }

        [TestMethod]
        public async Task AddAsync_ShouldReturnSuccess_WhenEntityIsAdded()
        {
            // Arrange
            var entity = new Product();
            _dateTimeProvider.Setup(s => s.UtcNow).Returns(DateTime.UtcNow).Verifiable(Times.Once);
            // Act
            var result = await _baseDAL.AddRecordAsync(entity);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(entity, result.Entity);
            Assert.IsNull(result.Exception);
            _dateTimeProvider.Verify();
            _dateTimeProvider.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddAsync_ShouldReturnFailure_WhenNoRowsAffected()
        {
            // Arrange
            var entity = new Product();
            // Simulate exception by disposing the context before SaveChangesAsync
            _context.Dispose();

            // Act
            var result = await _baseDAL.AddRecordAsync(entity);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNull(result.Entity);
            Assert.IsNotNull(result.Exception);
            _dateTimeProvider.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddAsync_ShouldReturnError_WhenExceptionIsThrown()
        {
            // Arrange
            var entity = new Product();

            // Simulate exception by disposing the context before SaveChangesAsync
            _context.Dispose();

            // Act
            var result = await _baseDAL.AddRecordAsync(entity);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNull(result.Entity);
            Assert.IsNotNull(result.Exception);
        }

        // TestBaseDAL is a testable implementation of BaseDAL
        private class TestBaseDAL : BaseDAL
        {
            public TestBaseDAL(GraphQlDatabaseContext databaseContext)
                : base(databaseContext) { }

            public async ValueTask<DbAddResult<T>> AddRecordAsync<T>(T entity)
                where T : class
            {
                return await AddAsync<T>(entity);
            }
        }
    }
}
