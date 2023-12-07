namespace BreakEven.UnitTests.Services;

using BreakEven.API.Services;
using Xunit;

public class TransportationAllowanceServiceTests
{
    private readonly TransportationAllowanceService SUT = new();
    
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