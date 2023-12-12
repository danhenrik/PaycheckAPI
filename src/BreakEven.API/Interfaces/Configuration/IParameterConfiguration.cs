using BreakEven.API.Shared;

namespace BreakEven.API.Interfaces.Configuration;

public interface IParameterConfiguration
{
    List<DiscountInfo>? GetIRRFDiscounts();
    List<DiscountInfo>? GetINSSDiscounts();
    double GetFGTSDiscountRate();
    double GetHealthInsuranceDiscount();
    double GetDentalInsuranceDiscount();
    TransportAllowanceInformation? GetTransportationAllowanceInformation();
}