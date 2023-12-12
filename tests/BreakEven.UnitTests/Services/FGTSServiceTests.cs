
using BreakEven.API.Interfaces.Configuration;

namespace BreakEven.UnitTests.Services;

using BreakEven.API.Services;
using BreakEven.API.Exceptions;
using Xunit;
using Moq;

public class FGTSServiceTests
{
    private readonly FGTSService SUT;

    public FGTSServiceTests()
    {
        Mock<IParameterConfiguration> configurationMock = new();
        configurationMock
            .Setup(x => x.GetFGTSDiscountRate())
            .Returns(0.08);

        SUT = new FGTSService(configurationMock.Object);
    }
    
    [Fact]
    public void Compute_WhenGivenValidSalary_ReturnsComputation()
    {
        const int salary = 2000;
        const int expectedDiscount = 160;

        var result = SUT.Compute(salary);

        Assert.Equal(expectedDiscount, result);
    }

    [Fact]
    public void Compute_WhenGivenInvalidSalary_ThrowsNegativeSalaryException()
    {
        const int salary = -1000;

        Assert.Throws<NegativeSalaryException>(() => SUT.Compute(salary));
    }
}