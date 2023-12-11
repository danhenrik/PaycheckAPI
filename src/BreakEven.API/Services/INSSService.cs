using BreakEven.API.Exceptions;
using BreakEven.API.Interfaces.Configuration;
using BreakEven.API.Interfaces.Services;
using BreakEven.API.Shared;

namespace BreakEven.API.Services;

public class INSSService : IINSSService
{
    private readonly Dictionary<int, DiscountInfo> LevelInfo = new();
    
    public INSSService(IParameterConfiguration configuration)
    {
        var irrfSection = configuration.GetINSSDiscounts();

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
            if (grossSalary > LevelInfo[level].MaxValue) continue;

            var discount = LevelInfo[level].DiscountRate * grossSalary;
            return discount;
        }

        return 0;
    }
}