using BreakEven.API.Exceptions;
using BreakEven.API.Interfaces.Services;
using BreakEven.API.Shared;

namespace BreakEven.API.Services;

public class IRRFService: IIRRFService
{
    private enum SalaryLevel
    {
        L1,
        L2,
        L3,
        L4,
        L5
    }
    
    private static readonly Dictionary<SalaryLevel, DiscountInfo> LevelInfo = new()
    {
        { SalaryLevel.L1, new DiscountInfo { Quota = 1903.89, DiscountRate = 0, DiscountLimit = 0 } },
        { SalaryLevel.L2, new DiscountInfo { Quota = 2826.65, DiscountRate = 0.075, DiscountLimit = 142.80 } },
        { SalaryLevel.L3, new DiscountInfo { Quota = 3751.05, DiscountRate = 0.15, DiscountLimit = 354.80 } },
        { SalaryLevel.L4, new DiscountInfo { Quota = 4664.68, DiscountRate = 0.225, DiscountLimit = 636.13 } },
        { SalaryLevel.L5, new DiscountInfo { Quota = double.MaxValue, DiscountRate = 0.275, DiscountLimit = 869.36 } }
    };

    public double Compute(double grossSalary)
    {
        if (grossSalary < 0)
            throw new NegativeSalaryException();

        foreach (var level in LevelInfo.Keys)
        {
            if (grossSalary > LevelInfo[level].Quota)
                continue;

            var discount = LevelInfo[level].DiscountRate * grossSalary;
            return Math.Min(discount, LevelInfo[level].DiscountLimit);
        }
        return 0;
    }
}
