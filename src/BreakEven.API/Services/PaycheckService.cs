using BreakEven.API.Entities;
using BreakEven.API.Entities.Enums;
using BreakEven.API.Interfaces.Services;

namespace BreakEven.API.Services;

public class PaycheckService(
        IIRRFService _irrfService,
        IINSSService _inssService,
        IFGTSService _fgtsService,
        ITransportationAllowanceService _transportationAllowanceService)
    : IPaycheckService
{
    public Paycheck GeneratePaycheck(Employee employee)
    {
        var GrossSalary = employee.GrossSalary;
        var paycheck = new Paycheck()
        {
            GrossSalary = GrossSalary, 
            Month = DateTime.Now.ToString("MM/yyyy")
        };
        
        var IRRFAmount = _irrfService.Compute(GrossSalary);
        paycheck.AddAdjustment(AdjustmentType.Discount, IRRFAmount, "IRRF");
        
        var INSSAmount = _inssService.Compute(GrossSalary);
        paycheck.AddAdjustment(AdjustmentType.Discount, INSSAmount, "INSS");
        
        var FGTSAmount = _fgtsService.Compute(GrossSalary);
        paycheck.AddAdjustment(AdjustmentType.Discount, FGTSAmount, "FGTS");
        
        if (employee.HasHealthInsurance)
        {
            const double healthInsuranceAmount = 10.0;
            paycheck.AddAdjustment(AdjustmentType.Discount, healthInsuranceAmount, "Health Insurance");
        }
        
        if (employee.HasDentalInsurance)
        {
            const double dentalInsuranceAmount = 5.0;
            paycheck.AddAdjustment(AdjustmentType.Discount, dentalInsuranceAmount, "Dental Insurance");
        }
        
        if (employee.HasTransportationAllowance)
        {
            var transportationAllowanceAmount = _transportationAllowanceService.Compute(GrossSalary);
            paycheck.AddAdjustment(AdjustmentType.Discount,transportationAllowanceAmount,"Transportation Allowance");
        }

        paycheck.ComputeAdjustments();
        return paycheck; 
    }
}