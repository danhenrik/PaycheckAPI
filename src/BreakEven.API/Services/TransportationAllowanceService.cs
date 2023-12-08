using BreakEven.API.Interfaces.Services;

namespace BreakEven.API.Services;

public class TransportationAllowanceService: ITransportationAllowanceService
{
    private const double MinimumSalary = 1500.00;

    public double Compute(double grossSalary)
    {
        if (grossSalary > MinimumSalary)
            return grossSalary * 0.06;
        return 0;
    }
}