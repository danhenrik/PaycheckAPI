namespace BreakEven.API.Strategies.Paycheck;

using BreakEven.API.Interfaces.Strategies.Paycheck;
using BreakEven.API.Entities;

public class UpdatePaycheckPayment : IUpdatePaycheckStrategy
{
    public double Amount { get; init; }
    public  void UpdatePaycheck(Paycheck paycheck)
    {
        paycheck.Pay(Amount);
    }
}