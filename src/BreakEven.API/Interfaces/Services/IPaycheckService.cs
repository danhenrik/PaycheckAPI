using BreakEven.API.Entities;

namespace BreakEven.API.Interfaces.Services;

public interface IPaycheckService
{
    public Paycheck GeneratePaycheck(Employee emp);
}