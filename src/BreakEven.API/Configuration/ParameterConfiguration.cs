using BreakEven.API.Interfaces.Configuration;
using BreakEven.API.Shared;

namespace BreakEven.API.Configuration;

public class ParameterConfiguration: IParameterConfiguration
{
    private readonly IConfiguration _configuration;
    
    public ParameterConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public List<DiscountInfo>? GetIRRFDiscounts()
    {
        return _configuration
            .GetSection("IRRF")
            .Get<List<DiscountInfo>>();
    }

    public List<DiscountInfo>? GetINSSDiscounts()
    {
        return _configuration
            .GetSection("INSS")
            .Get<List<DiscountInfo>>();
    }

    public double GetFGTSDiscountRate()
    {
        return _configuration
            .GetValue<double>("FGTSDiscountRate");
    }

    public double GetHealthInsuranceDiscount()
    {
        return _configuration
            .GetValue<double>("HealthInsuranceDiscount");
    }

    public double GetDentalInsuranceDiscount()
    {
        return _configuration
            .GetValue<double>("DentalInsuranceDiscount");
    }

    public TransportAllowanceInformation? GetTransportationAllowanceInformation()
    {
        return _configuration
            .GetSection("TransportationVoucher")
            .Get<TransportAllowanceInformation>();
    }
}