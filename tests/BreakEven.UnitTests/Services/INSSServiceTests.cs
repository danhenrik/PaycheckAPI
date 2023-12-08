namespace BreakEven.UnitTests.Services;

using BreakEven.API.Exceptions;
using BreakEven.API.Services;
using System;
using Xunit;

public class INSSServiceTests
{
    private readonly INSSService SUT = new();
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