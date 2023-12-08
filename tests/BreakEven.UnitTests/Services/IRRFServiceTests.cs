namespace BreakEven.UnitTests.Services;

using BreakEven.API.Exceptions;
using BreakEven.API.Services;
using System;
using Xunit;

public class IRRFServiceTests
{
    private readonly IRRFService SUT = new();
    
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