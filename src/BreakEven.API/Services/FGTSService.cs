using BreakEven.API.Interfaces.Services;

namespace BreakEven.API.Services;

public class FGTSService: IFGTSService
{
    public double Compute(double grossSalary)
    {
        if (grossSalary < 0)
            throw new ArgumentException("Salary cannot be less than 0", nameof(grossSalary));

        return grossSalary * 0.08;
    }
}