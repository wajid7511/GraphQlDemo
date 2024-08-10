using GraphQl.Abstractions;
using GraphQl.Database.DAL;
using GraphQl.Database.Models;
using GraphQlDemo.Shared.Database;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GraphQl.Database.Test.DALs
{
    [TestClass]
    public class BaseDALTests
    {
        private TestBaseDAL _baseDAL = null!;

        private GraphQlDatabaseContext _context = null!;
        private readonly Mock<IDateTimeProvider> _dateTimeProvider = new();

        [TestInitialize]
        public void Setup()
        {
            _dateTimeProvider.Setup(s => s.UtcNow).Returns(DateTime.UtcNow);
            _context = MockFactory.GraphQlDatabaseContext(_dateTimeProvider);
            _baseDAL = new TestBaseDAL(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }

        [TestMethod]
        public async Task AddAsync_ShouldReturnSuccess_WhenEntityIsAdded()
        {
            // Arrange
            var entity = new Product();
            // Act
            var result = await _baseDAL.AddRecordAsync(entity);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(entity, result.Entity);
            Assert.IsNull(result.Exception);
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
