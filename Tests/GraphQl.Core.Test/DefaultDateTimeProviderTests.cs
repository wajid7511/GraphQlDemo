using GraphQl.Abstractions;

namespace GraphQl.Core.Test;

[TestClass]
public class DefaultDateTimeProviderTests
{
    private IDateTimeProvider _dateTimeProvider = null!;

    [TestInitialize]
    public void Setup()
    {
        _dateTimeProvider = new DefaultDateTimeProvider();
    }

    [TestMethod]
    public void Now_ShouldReturnCurrentLocalTime()
    {
        // Act
        DateTime now = _dateTimeProvider.Now;

        // Assert
        Assert.AreEqual(
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            now.ToString("yyyy-MM-dd HH:mm:ss"),
            "The Now property did not return the expected local time."
        );
    }

    [TestMethod]
    public void UtcNow_ShouldReturnCurrentUtcTime()
    {
        // Act
        DateTime utcNow = _dateTimeProvider.UtcNow;

        // Assert
        Assert.AreEqual(
            DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
            utcNow.ToString("yyyy-MM-dd HH:mm:ss"),
            "The UtcNow property did not return the expected UTC time."
        );
    }
}
