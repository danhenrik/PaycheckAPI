namespace BreakEven.UnitTests.Services;

using BreakEven.API.Services;
using System;
using Xunit;

public class FGTSServiceTests
{
    private readonly FGTSService SUT = new();
    
    [Fact]
    public void Compute_WhenGivenValidSalary_ReturnsComputation()
    {
        const int salary = 2000;
        const int expectedDiscount = 160;

        var result = SUT.Compute(salary);

        Assert.Equal(expectedDiscount, result);
    }

    [Fact]
    public void Compute_WhenGivenInvalidSalary_ThrowsArgumentException()
    {
        const int salary = -1000;

        Assert.Throws<ArgumentException>(() => SUT.Compute(salary));
    }
}