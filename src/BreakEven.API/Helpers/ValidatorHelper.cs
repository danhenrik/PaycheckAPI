using BreakEven.API.ViewModels;
using FluentValidation;

namespace BreakEven.API.Helpers;

public class EmployeeValidator : AbstractValidator<CreateEmployeeViewModel>
{
    public EmployeeValidator()
    {
        RuleFor(emp => emp.FirstName)
            .NotEmpty()
            .WithMessage("Please specify first name");
        RuleFor(emp => emp.SurName)
            .NotEmpty()
            .WithMessage("Please specify surname");
        RuleFor(emp => emp.CPF)
            .NotEmpty()
            .WithMessage("Please specify cpf")
            .Must(BeAValidCPF)
            .WithMessage("Please specify a valid cpf");
        RuleFor(emp => emp.Sector)
            .NotEmpty()
            .WithMessage("Please specify sector");
        RuleFor(emp => emp.GrossSalary)
            .NotEmpty()
            .WithMessage("Please specify GrossSalary")
            .GreaterThan(0)
            .WithMessage("Gross salary must be greater than 0");
        RuleFor(emp => emp.AdmissionDate)
            .NotEmpty()
            .WithMessage("Please specify admission date");
        RuleFor(emp => emp.HasHealthInsurance)
            .NotEmpty()
            .WithMessage("Please specify hasHealthInsurance");
        RuleFor(emp => emp.HasDentalInsurance)
            .NotEmpty()
            .WithMessage("Please specify hasDentalInsurance");
        RuleFor(emp => emp.HasTransportationAllowance)
            .NotEmpty()
            .WithMessage("Please specify hasTransportationAllowance");
    } 
    private bool BeAValidCPF(string cpf)
    {
        return cpf.Length == 11 && cpf.All(Char.IsDigit);
    }
}