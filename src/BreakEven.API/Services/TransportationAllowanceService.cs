using BreakEven.API.Exceptions;
using BreakEven.API.Interfaces.Configuration;
using BreakEven.API.Interfaces.Services;
using BreakEven.API.Shared;

namespace BreakEven.API.Services;

public class TransportationAllowanceService : ITransportationAllowanceService
{
    private readonly double _minimumSalary;

    private readonly double _transportationAllowanceDiscountRate;
    
    public TransportationAllowanceService(IParameterConfiguration configuration)
    {
        var transportationAllowanceInformation = configuration.GetTransportationAllowanceInformation();

        if (transportationAllowanceInformation == null) 
            throw new UnspecifiedInformationException(this);
    
        _minimumSalary = transportationAllowanceInformation.MinimumSalary;
        _transportationAllowanceDiscountRate = transportationAllowanceInformation.DiscountRate;
    }

    public double Compute(double grossSalary)
    {
        if (grossSalary > _minimumSalary) 
            return grossSalary * _transportationAllowanceDiscountRate;

        return 0;
    }
}