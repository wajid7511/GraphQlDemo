using GraphQlDemo.Shared.Enums;

namespace GraphQlDemo.Shared.Tests.Enums;

[TestClass]
public class OrderStatusEnumTest
{
    [TestInitialize]
    public void Init()
    {
        //
    }
    [TestCleanup]
    public void Cleanup()
    {
        //
    }
    [TestMethod]
    public void Check_Values()
    {
        //Arrange 

        //Act

        //Assert
        Assert.AreEqual(1, (int)OrderStatusEnum.Created);
        Assert.AreEqual(2, (int)OrderStatusEnum.Processing);
        Assert.AreEqual(3, (int)OrderStatusEnum.Placed);
        Assert.AreEqual(4, (int)OrderStatusEnum.Failed);
    }
}