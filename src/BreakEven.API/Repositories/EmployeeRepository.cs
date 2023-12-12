using BreakEven.API.Data;
using BreakEven.API.Entities;
using BreakEven.API.Interfaces.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace BreakEven.API.Repositories;

[ExcludeFromCodeCoverage]
public class EmployeeRepository(AppDbContext db) : IEmployeeRepository
{
    public Task Create(Employee employee)
    {
        db.Employees.Add(employee);
        return db.SaveChangesAsync();
    }

    public Employee? GetById(string id)
    {
        return db.Employees.FirstOrDefault(emp => emp!.Id == id);
    }

    public Employee? GetByCpf(string cpf)
    {
        return db.Employees.FirstOrDefault(emp => emp!.CPF == cpf);
    }

    public List<Employee> GetAll()
    {
        return db.Employees.ToList();
    }
}