using BreakEven.API.Interfaces.Services;

namespace BreakEven.API.Services;

public class TransportationAllowanceService: ITransportationAllowanceService
{
    private const double MinimumSalary = 1500.00;

    private const double _transportationAllowanceDiscountRate = 0.06;
    
    public double Compute(double grossSalary)
    {
        if (grossSalary > MinimumSalary)
            return grossSalary * _transportationAllowanceDiscountRate;
        return 0;
    }
}