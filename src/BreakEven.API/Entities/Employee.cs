using BreakEven.API.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace BreakEven.API.Entities;

public class Employee
{
    [Key]
    public string CPF { get; set;  }
    
    public string FirstName { get; set; }
    
    public string SurName { get; set; }
    
    public string Sector { get; set; } 
    
    public double GrossSalary { get; set; }
    
    public DateTime AdmissionDate { get; set; }
    
    public bool HasHealthInsurance { get; set; }
    
    public bool HasDentalInsurance { get; set; }
    
    public bool HasTransportationAllowance { get; set; }
}