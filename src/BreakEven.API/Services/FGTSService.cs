using BreakEven.API.Exceptions;
using BreakEven.API.Interfaces.Services;

namespace BreakEven.API.Services;

public class FGTSService : IFGTSService
{
    private const  double _fgtsDiscountRate = 0.08;
    
    public double Compute(double grossSalary)
    {
        if (grossSalary < 0)
            throw new NegativeSalaryException();

        return grossSalary * _fgtsDiscountRate;
    }
}