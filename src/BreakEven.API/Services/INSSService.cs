using BreakEven.API.Exceptions;
using BreakEven.API.Interfaces.Services;
using BreakEven.API.Shared;

namespace BreakEven.API.Services;

public class INSSService: IINSSService
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
        { SalaryLevel.L1, new DiscountInfo { Quota = 1045.00, DiscountRate = 0.075 } },
        { SalaryLevel.L2, new DiscountInfo { Quota = 2089.60, DiscountRate = 0.09 } },
        { SalaryLevel.L3, new DiscountInfo { Quota = 3134.40, DiscountRate = 0.12 } },
        { SalaryLevel.L4, new DiscountInfo { Quota = 6101.06, DiscountRate = 0.14 } },
        { SalaryLevel.L5, new DiscountInfo { Quota = double.MaxValue, DiscountRate = 0.17 } }
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
            return discount;
        }
        return 0;
    }       
}