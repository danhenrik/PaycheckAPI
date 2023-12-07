using BreakEven.API.Entities;

namespace BreakEven.API.Interfaces.Repositories;

public interface IEmployeeRepository
{
    public Task Create(Employee? employee);
    public Employee? GetByCpf(string cpf);
    public List<Employee> GetAll();
}