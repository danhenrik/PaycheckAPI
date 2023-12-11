using BreakEven.API.Interfaces.Configuration;
using BreakEven.API.Shared;
using Moq;

namespace BreakEven.UnitTests.Services;

using BreakEven.API.Services;
using Xunit;

public class TransportationAllowanceServiceTests
{
    private readonly TransportationAllowanceService SUT;
    
    public TransportationAllowanceServiceTests()
    {
        var trasnportInfo = new TransportAllowanceInformation()
        {
            MinimumSalary = 1500, 
            DiscountRate= 0.06
        };
        Mock<IParameterConfiguration> configurationMock = new();
        configurationMock
            .Setup(x => x.GetTransportationAllowanceInformation())
            .Returns(trasnportInfo);
        
        SUT = new TransportationAllowanceService(configurationMock.Object);
    }
    
    [Fact]
    public void Compute_WhenGivenValidSalary_ReturnsComputation()
    {
        const int salary = 2000;
        const int expectedDiscount = 120;

        var result = SUT.Compute(salary);

        Assert.Equal(expectedDiscount, result);
    }

    [Fact]
    public void Compute_WhenGivenSalarySmallerThanMinimum_ReturnsZero()
    {
        const int salary = 1300;

        var result = SUT.Compute(salary);

        Assert.Equal(0, result);
    }
}