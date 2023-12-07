namespace BreakEven.UnitTests.Entities;

using BreakEven.API.Entities;
using BreakEven.API.Strategies.Paycheck;
using System;
using Xunit;

public class AdjustmentTests
{
    [Fact]
    public void GetUpdatePaycheckStrategy_WhenGivenInvalidType_ThrowsArgumentOutOfRangeException()
    {
        var adjustment = new Adjustment()
        {
            Type = (AdjustmentType) 3
        };
        
        Assert.Throws<ArgumentOutOfRangeException>(() => adjustment.GetUpdatePaycheckStrategy());
    }
    
    [Fact]
    public void GetUpdatePaycheckStrategy_WhenGivenDiscountType_ReturnsDiscountStrategy()
    {
        var adjustment = new Adjustment()
        {
            Type = AdjustmentType.Discount
        };

        var result = adjustment.GetUpdatePaycheckStrategy();
        
        Assert.IsType<UpdatePaycheckDiscount>(result);
    }
    
    [Fact]
    public void GetUpdatePaycheckStrategy_WhenGivenPaymentType_ReturnsPaymentStrategy()
    {
        var adjustment = new Adjustment()
        {
            Type = AdjustmentType.Pay
        };

        var result = adjustment.GetUpdatePaycheckStrategy();
        
        Assert.IsType<UpdatePaycheckPayment>(result);
    }
}