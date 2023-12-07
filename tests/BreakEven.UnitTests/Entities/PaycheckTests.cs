namespace BreakEven.UnitTests.Entities;

using BreakEven.API.Entities;
using Xunit;

public class PaycheckTests
{
    private readonly Paycheck _paycheck = new() { GrossSalary = 1000 };
    
    [Fact]
    public void AddAdjustment_WhenAddedOnce_HaveOneAdjustment() 
    {
        _paycheck.AddAdjustment(AdjustmentType.Discount, 100, "Test");
        
        Assert.Equal(1, _paycheck.Adjustments.Count);
    }
    
    [Fact]
    public void AddAdjustment_WhenAddedTwice_HaveTwoAdjustment() 
    {
        _paycheck.AddAdjustment(AdjustmentType.Discount, 100, "TestDiscount");
        _paycheck.AddAdjustment(AdjustmentType.Pay, 100, "TestPayment");

        Assert.Equal(2, _paycheck.Adjustments.Count);
    }
    
    [Fact]
    public void Discount_WhenCalled_ShouldDecreaseNetSalaryAndTotalDiscounts() 
    {
        _paycheck.Discount(100);
        
        Assert.Equal(-100, _paycheck.NetSalary);
        Assert.Equal(-100, _paycheck.TotalDiscounts);
    }
    
    [Fact]
    public void Pay_WhenCalled_ShouldIncreaseNetSalary() 
    {
        _paycheck.Pay(100);
        
        Assert.Equal(100, _paycheck.NetSalary);
    }
    
    [Fact]
    public void ComputeAdjustments_WhenCalled_ShouldComputeNetSalary() 
    {
        _paycheck.AddAdjustment(AdjustmentType.Discount, 100, "TestDiscount");
        _paycheck.AddAdjustment(AdjustmentType.Pay, 2, "TestPayment");
        
        _paycheck.ComputeAdjustments();
        
        Assert.Equal(902, _paycheck.NetSalary);
        Assert.Equal(-100, _paycheck.TotalDiscounts);
        Assert.Equal(10, _paycheck.Adjustments[0].Percentage);
        Assert.Equal(0.2, _paycheck.Adjustments[1].Percentage);
    }
    
    [Fact]
    public void ComputeAdjustments_WhenCalled_ShouldComputeTotalDiscounts() 
    {
        _paycheck.AddAdjustment(AdjustmentType.Discount, 100, "TestDiscount");
        _paycheck.AddAdjustment(AdjustmentType.Pay, 200, "TestPayment");
        
        _paycheck.ComputeAdjustments();
        
        Assert.Equal(-100, _paycheck.TotalDiscounts);
    }
    
    [Fact]
    public void ComputeAdjustments_WhenCalled_ShouldComputePercentages() 
    {
        _paycheck.AddAdjustment(AdjustmentType.Discount, 100, "TestDiscount");
        _paycheck.AddAdjustment(AdjustmentType.Pay, 52, "TestPayment");
        
        _paycheck.ComputeAdjustments();
        
        Assert.Equal(10, _paycheck.Adjustments[0].Percentage);
        Assert.Equal(5.2, _paycheck.Adjustments[1].Percentage);
    }
}