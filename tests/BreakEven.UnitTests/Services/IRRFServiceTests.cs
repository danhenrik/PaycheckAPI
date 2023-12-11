using BreakEven.API.Interfaces.Configuration;
using BreakEven.API.Shared;

namespace BreakEven.UnitTests.Services;

using BreakEven.API.Exceptions;
using BreakEven.API.Services;
using Xunit;
using Moq;

public class IRRFServiceTests
{
    private readonly IRRFService SUT;

    public IRRFServiceTests()
    {          
        var irrfInfo = new List<DiscountInfo>
        {
            new DiscountInfo() {DiscountRate = 0, DiscountLimit = 0, MaxValue = 1900},
            new DiscountInfo() { DiscountRate = 0.075, DiscountLimit = 142.80, MaxValue = 2800 },
            new DiscountInfo() { DiscountRate = 0.15, DiscountLimit = 354.80, MaxValue = 3700 },
            new DiscountInfo() { DiscountRate = 0.225, DiscountLimit = 636.13, MaxValue = 4600 },
            new DiscountInfo() { DiscountRate = 0.275, DiscountLimit = 869.36, MaxValue = 0 }
        };
        
        Mock<IParameterConfiguration> configurationMock = new();
        configurationMock
            .Setup(x => x.GetIRRFDiscounts())
            .Returns(irrfInfo);
        
        SUT = new IRRFService(configurationMock.Object);
    }
    
    [Fact]
    public void Compute_WhenGivenInvalidSalary_ThrowsNegativeSalaryException()
    {
        const int salary = -1000;

        Assert.Throws<NegativeSalaryException>(() => SUT.Compute(salary));
    }
    
    [Theory]
    [InlineData(1000, 0)]       // L1
    [InlineData(2500, 142.80)]  // L2
    [InlineData(3500, 354.80)]  // L3
    [InlineData(4500, 636.13)]  // L4
    [InlineData(7000, 869.36)]  // L5
    public void Compute_WhenGivenValidSalary_ReturnsDiscountLimitIfComputedIsBigger(double salary, double expectedDiscount)
    {
        var result = SUT.Compute(salary);

        Assert.Equal(expectedDiscount, result);
    }
}