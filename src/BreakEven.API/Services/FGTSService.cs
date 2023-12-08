using BreakEven.API.Exceptions;
using BreakEven.API.Interfaces.Services;

namespace BreakEven.API.Services;

public class FGTSService: IFGTSService
{
    public double Compute(double grossSalary)
    {
        if (grossSalary < 0)
            throw new NegativeSalaryException();

        return grossSalary * 0.08;
    }
}