namespace BreakEven.API.Entities;

public class Paycheck
{
    public string Month { get; init; }
    
    public List<Adjustment> Adjustments { get; } = new();

    public int DiscountCount { get; set; } = 0;
    
    public double GrossSalary { get; init; }
    
    public double TotalDiscounts { get; private set; }
    
    public double NetSalary { get; private set; }
    
    public void AddAdjustment(AdjustmentType type, double amount, string description)
    {
        Adjustment adjustment = new Adjustment() { Type = type, Amount = amount, Description = description }; 
        Adjustments.Add(adjustment);
    }

    public void ComputeAdjustments()
    {
        TotalDiscounts = 0;
        NetSalary = GrossSalary;
        foreach (var adjustment in Adjustments)
        {
            var strategy = adjustment.GetUpdatePaycheckStrategy();
            strategy.UpdatePaycheck(this);
        }

        foreach (var adjustment in Adjustments)
            adjustment.Percentage = (adjustment.Amount / GrossSalary) * 100;
    }

    public void Discount(double amount)
    {
        TotalDiscounts -= amount;
        NetSalary -= amount;
        DiscountCount++;
    }

    public void Pay(double amount)
    {
        NetSalary += amount;
    }
}