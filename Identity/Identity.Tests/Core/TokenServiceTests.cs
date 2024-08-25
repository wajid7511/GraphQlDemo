using GraphQl.Abstractions;
using GraphQlDemo.Shared.Jwt;
using Identity.Core;
using Microsoft.Extensions.Options;
using Moq;
using System.Security.Claims;

namespace GraphQl.Common.Test.JwtToken
{
    [TestClass]
    public class TokenServiceTests
    {
        private TokenService _tokenService = null!;
        private readonly Mock<IOptions<JtwTokenOptions>> _mockOptions = new();
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider = new();

        [TestInitialize]
        public void Setup()
        {
            var jwtTokenOptions = new JtwTokenOptions
            {
                SecretKey = "ffc9ca3413d0c808cbdb4d8011846ac2285f4fb0cbe6ae512d216e98a08828fb",
                TokenExpirationMinutes = 300000,
                RefreshTokenExpirationDays = 30,
                Issuer = "yourIssuer",
                Audience = "yourAudience"
            };

            _mockOptions.Setup(o => o.Value).Returns(jwtTokenOptions);

            _tokenService = new TokenService(_mockOptions.Object, _mockDateTimeProvider.Object);
        }

        [TestMethod]
        public void GenerateJwtToken_ShouldReturnValidToken()
        {
            // Arrange 
            Guid userId = Guid.NewGuid();
            _mockDateTimeProvider.Setup(s => s.UtcNow).Returns(DateTime.UtcNow)
            .Verifiable(Times.Once);
            // Act
            var token = _tokenService.GenerateJwtToken(userId);

            // Assert
            Assert.IsNotNull(token);
            Assert.IsInstanceOfType(token.Item1, typeof(string));
            Assert.IsInstanceOfType(token.Item2, typeof(DateTimeOffset));

            var principal = _tokenService.ValidateJwtToken(token.Item1);

            Assert.IsNotNull(principal);
            Assert.AreEqual(userId.ToString(), principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Assert.AreEqual(JwtClaimConstant.Basic_Role, principal.FindFirst(ClaimTypes.Role)?.Value);
            _mockDateTimeProvider.Verify();
            _mockDateTimeProvider.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GenerateRefreshToken_ShouldReturnValidRefreshToken()
        {
            // Act
            _mockDateTimeProvider.Setup(s => s.UtcNow).Returns(DateTime.UtcNow)
            .Verifiable(Times.Once);
            Guid userId = Guid.NewGuid();
            var token = _tokenService.GenerateRefreshToken(userId);
            //Assert
            Assert.IsNotNull(token);
            Assert.IsInstanceOfType(token.Item1, typeof(string));
            Assert.IsInstanceOfType(token.Item2, typeof(DateTimeOffset));

            // Assert 
            var principal = _tokenService.ValidateJwtToken(token.Item1);

            Assert.IsNotNull(principal);
            Assert.AreEqual(userId.ToString(), principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Assert.AreEqual(JwtClaimConstant.Basic_Role, principal.FindFirst(ClaimTypes.Role)?.Value);
            _mockDateTimeProvider.Verify();
            _mockDateTimeProvider.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void ValidateJwtToken_ShouldReturnClaimsPrincipal_WhenTokenIsValid()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            _mockDateTimeProvider.Setup(s => s.UtcNow).Returns(DateTime.UtcNow)
            .Verifiable(Times.Once);
            var token = _tokenService.GenerateJwtToken(userId);

            // Act
            var principal = _tokenService.ValidateJwtToken(token.Item1);

            // Assert
            Assert.IsNotNull(principal);
            Assert.AreEqual(userId.ToString(), principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Assert.AreEqual(JwtClaimConstant.Basic_Role, principal.FindFirst(ClaimTypes.Role)?.Value);
            _mockDateTimeProvider.Verify();
            _mockDateTimeProvider.VerifyNoOtherCalls();

        }

        [TestMethod]
        public void ValidateJwtToken_ShouldReturnNull_WhenTokenIsInvalid()
        {
            // Arrange
            var invalidToken = "invalid_token";

            // Act
            var principal = _tokenService.ValidateJwtToken(invalidToken);

            // Assert
            Assert.IsNull(principal);
            _mockDateTimeProvider.VerifyNoOtherCalls();
        }
    }
}
