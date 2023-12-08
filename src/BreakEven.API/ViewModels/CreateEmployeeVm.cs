using BreakEven.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace BreakEven.API.ViewModels;

public class CreateEmployeeVm
{
    [Required]
    public string CPF { get; init; }

    [Required]
    public string FirstName { get; init; }

    [Required]
    public string SurName { get; init; }

    [Required]
    public string Sector { get; init; }

    [Required]
    public double GrossSalary { get; init; }

    [Required]
    public DateTime AdmissionDate { get; init; }

    [Required]
    public bool HasHealthInsurance { get; init; }

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