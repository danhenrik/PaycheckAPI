using BreakEven.API.Exceptions;
using BreakEven.API.Interfaces.Configuration;
using BreakEven.API.Interfaces.Services;
using BreakEven.API.Shared;

namespace BreakEven.API.Services;

public class IRRFService: IIRRFService
{
    private readonly Dictionary<int, DiscountInfo> LevelInfo = new();

    public IRRFService(IParameterConfiguration configuration)
    {
        var irrfSection = configuration.GetIRRFDiscounts();

        if (irrfSection == null)
            throw new UnspecifiedInformationException(this);

        var levelIndex = 1;
        foreach (var levelInfo in irrfSection)
        {
            if (levelInfo.MaxValue == 0)
                levelInfo.MaxValue = double.MaxValue;
            
            LevelInfo.Add(levelIndex, levelInfo);
            levelIndex++;
        }
    }
    
    public double Compute(double grossSalary)
    {
        if (grossSalary < 0)
            throw new NegativeSalaryException();

        foreach (var level in LevelInfo.Keys)
        {
            if (grossSalary > LevelInfo[level].MaxValue)
                continue;

            var discount = LevelInfo[level].DiscountRate * grossSalary;
            return Math.Min(discount, LevelInfo[level].DiscountLimit);
        }
        return 0;
    }
}
