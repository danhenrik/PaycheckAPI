using BreakEven.API.Entities.Enums;
using BreakEven.API.Interfaces.Strategies.Paycheck;
using BreakEven.API.Strategies.Paycheck;

namespace BreakEven.API.Entities;

public class Adjustment
{
    public AdjustmentType Type { get; set; }
    public double Amount { get; init; }
    public string Description { get; init; }
    public double Percentage { get; set; }

    public IUpdatePaycheckStrategy GetUpdatePaycheckStrategy()
    {
        IUpdatePaycheckStrategy strategy = Type switch
        {
            AdjustmentType.Discount => new UpdatePaycheckDiscount() { Amount = Amount },
            AdjustmentType.Pay => new UpdatePaycheckPayment() { Amount = Amount },
            _ => throw new ArgumentOutOfRangeException()
        };

        return strategy;
    }
}