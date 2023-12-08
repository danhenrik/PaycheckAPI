using System.ComponentModel.DataAnnotations;

namespace BreakEven.API.Entities;

public class Employee
{
    [Key]
    public string CPF { get; init;  }
    
    public string FirstName { get; init; }
    
    public string SurName { get; init; }
    
    public string Sector { get; init; } 
    
    public double GrossSalary { get; init; }
    
    public DateTime AdmissionDate { get; init; }
    
    public bool HasHealthInsurance { get; init; }
    
    public bool HasDentalInsurance { get; init; }
    
    public bool HasTransportationAllowance { get; init; }
}