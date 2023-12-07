namespace BreakEven.API.Interfaces.Strategies.Paycheck;

using BreakEven.API.Entities;

public interface IUpdatePaycheckStrategy
{
    public void UpdatePaycheck(Paycheck paycheck);
}