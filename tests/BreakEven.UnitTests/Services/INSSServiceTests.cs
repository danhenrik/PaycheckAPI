using BreakEven.API.Interfaces.Configuration;
using BreakEven.API.Shared;

namespace BreakEven.UnitTests.Services;

using BreakEven.API.Exceptions;
using BreakEven.API.Services;
using Xunit;
using Moq;

public class INSSServiceTests
{
    private readonly INSSService SUT;

    public INSSServiceTests()
    {
        var inssInfo = new List<DiscountInfo>()
        {
            new DiscountInfo() { DiscountRate = 0.075, MaxValue = 1500 },
            new DiscountInfo() { DiscountRate = 0.09, MaxValue = 2500 },
            new DiscountInfo() { DiscountRate = 0.12, MaxValue = 3500 },
            new DiscountInfo() { DiscountRate = 0.14, MaxValue = 6100 },
            new DiscountInfo() { DiscountRate = 0.17, MaxValue = 0 }
        };
        Mock<IParameterConfiguration> configurationMock = new();
        configurationMock
            .Setup(x => x.GetINSSDiscounts())
            .Returns(inssInfo);
        
        SUT = new INSSService(configurationMock.Object);
    }
    
    [Fact]
    public void Compute_WhenGivenInvalidSalary_ThrowsNegativeSalaryException()
    {
        const int salary = -1000;

        Assert.Throws<NegativeSalaryException>(() => SUT.Compute(salary));
    }
    
    [Theory]
    [InlineData(1000, 75)]   // L1
    [InlineData(2000, 180)]  // L2
    [InlineData(3000, 360)]  // L3
    [InlineData(4000, 560)]  // L4
    [InlineData(7000, 1190)] // L5
    public void Compute_WhenGivenValidSalary_ReturnsComputation(double salary, double expectedDiscount)
    {
        var result = SUT.Compute(salary);

        Assert.Equal(expectedDiscount, result);
    }
}