using BreakEven.API.Entities;
using BreakEven.API.Entities.Enums;
using BreakEven.API.Interfaces.Configuration;
using BreakEven.API.Interfaces.Services;

namespace BreakEven.API.Services;

public class PaycheckService(
    IIRRFService _irrfService,
    IINSSService _inssService,
    IFGTSService _fgtsService,
    ITransportationAllowanceService _transportationAllowanceService,
    IParameterConfiguration _configuration) : IPaycheckService
{
    public Paycheck GeneratePaycheck(Employee employee)
    {
        var GrossSalary = employee.GrossSalary;
        var paycheck = new Paycheck() { GrossSalary = GrossSalary, Month = DateTime.Now.ToString("MM/yyyy") };

        var IRRFAmount = _irrfService.Compute(GrossSalary);
        paycheck.AddAdjustment(AdjustmentType.Discount, IRRFAmount, "IRRF");

        var INSSAmount = _inssService.Compute(GrossSalary);
        paycheck.AddAdjustment(AdjustmentType.Discount, INSSAmount, "INSS");

        var FGTSAmount = _fgtsService.Compute(GrossSalary);
        paycheck.AddAdjustment(AdjustmentType.Discount, FGTSAmount, "FGTS");

        if (employee.HasHealthInsurance)
        {
             double healthInsuranceAmount = _configuration.GetHealthInsuranceDiscount();
            paycheck.AddAdjustment(AdjustmentType.Discount, healthInsuranceAmount, "Health Insurance");
        }

        if (employee.HasDentalInsurance)
        {
            double dentalInsuranceAmount = _configuration.GetDentalInsuranceDiscount();
            paycheck.AddAdjustment(AdjustmentType.Discount, dentalInsuranceAmount, "Dental Insurance");
        }

        if (employee.HasTransportationAllowance)
        {
            var transportationAllowanceAmount = _transportationAllowanceService.Compute(GrossSalary);
            paycheck.AddAdjustment(AdjustmentType.Discount, transportationAllowanceAmount, "Transportation Allowance");
        }

        paycheck.ComputeAdjustments();
        return paycheck;
    }
}