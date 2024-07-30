using AutoMapper;

namespace GraphQlDemo.Test;

[TestClass]
public class GraphQlDemoProfileTests
{
    private IMapper _mapper = null!;
    private MapperConfiguration _config = null!;

    [TestInitialize]
    public void Setup()
    {
        _config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new GraphQlDemoProfile());
        });

        _mapper = _config.CreateMapper();
    } 
    [TestMethod]
    public void Asser_Mapper_Configurations() 
    { 
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
