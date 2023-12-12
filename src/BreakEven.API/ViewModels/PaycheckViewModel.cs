using BreakEven.API.Entities;
using BreakEven.API.Entities.Enums;

namespace BreakEven.API.ViewModels;

public class AdjustmentVm
{
    public string Type { get; set; }
    public string Amount { get; set; }
    public string Description { get; set; }
    public string Percentage { get; set; }
    public static AdjustmentVm FromDomain(Adjustment adjustment)
    {
        return new AdjustmentVm()
        {
            Type = adjustment.Type == AdjustmentType.Discount ? "Discount": "Payment",
            Amount = $"R$ {adjustment.Amount:n2}",
            Description = adjustment.Description,
            Percentage = $"{adjustment.Percentage:f}%"
        };
    }
}

public class PaycheckViewModel
{
    public string Month { get; set; } // MM/YYYY
    public List<AdjustmentVm> Adjustments { get; set; }
    public string GrossSalary { get; set; }
    public string TotalDiscounts { get; set; }
    public string NetSalary { get; set; }
    public int Discounts { get; set; }

    public static PaycheckViewModel FromDomain(Paycheck paycheck)
    {
        var paycheckVm = new PaycheckViewModel()
        {
            GrossSalary = $"R$ {paycheck.GrossSalary:n2}",
            NetSalary = $"R$ {paycheck.NetSalary:n2}",
            TotalDiscounts = $"R$ {paycheck.TotalDiscounts:n2}",
            Month = paycheck.Month,
            Adjustments = new List<AdjustmentVm>(),
            Discounts = paycheck.DiscountCount
        };
        
        paycheck.Adjustments.ForEach(
            adj => paycheckVm.Adjustments.Add(AdjustmentVm.FromDomain(adj))
        );

        return paycheckVm;
    }
}
