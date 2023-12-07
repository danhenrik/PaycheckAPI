using BreakEven.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace BreakEven.API.ViewModels;

public class CreateEmployeeVm
{
    [Required]
    public string CPF { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string SurName { get; set; }

    [Required]
    public string Sector { get; set; } // could possibly be swapped for a enum

    [Required]
    public double GrossSalary { get; set; }

    [Required]
    public DateTime AdmissionDate { get; set; }

    [Required]
    public bool HasHealthInsurance { get; set; }

    [Required]
    public bool HasDentalInsurance { get; set; }

    [Required]
    public bool HasTransportationAllowance { get; set; }

    public Employee ToDomain()
    {
        return new Employee()
        {
            CPF = CPF,
            FirstName = FirstName,
            SurName = SurName,
            Sector = Sector,
            GrossSalary = GrossSalary,
            AdmissionDate = AdmissionDate,
            HasHealthInsurance = HasHealthInsurance,
            HasDentalInsurance = HasDentalInsurance,
            HasTransportationAllowance = HasTransportationAllowance
        };
    }
}