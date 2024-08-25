using System;
using AutoMapper;
using Identity.Mappers;

namespace Identity.Tests.Mappers;
[TestClass]
public class IdentityProfileTests
{
    private IMapper _mapper = null!;
    private MapperConfiguration _config = null!;

    [TestInitialize]
    public void Setup()
    {
        _config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new IdentityProfile());
        });

        _mapper = _config.CreateMapper();
    }

    [TestCleanup]
    public void Cleanup()
    {
        //
    }

    [TestMethod]
    public void Asser_Mapper_Configurations()
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
