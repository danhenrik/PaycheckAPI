using BreakEven.API.Exceptions;
using BreakEven.API.Interfaces.Configuration;
using BreakEven.API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreakEven.API.Services;

public class FGTSService : IFGTSService
{
    private readonly double _fgtsDiscountRate;

    public FGTSService(IParameterConfiguration configuration)
    {
        _fgtsDiscountRate = configuration.GetFGTSDiscountRate();
    }

    public double Compute(double grossSalary)
    {
        if (grossSalary < 0)
            throw new NegativeSalaryException();

        return grossSalary * _fgtsDiscountRate;
    }
}