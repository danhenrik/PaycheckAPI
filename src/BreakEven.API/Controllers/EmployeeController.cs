using BreakEven.API.Exceptions;
using BreakEven.API.Helpers;
using BreakEven.API.Interfaces.Repositories;
using BreakEven.API.Interfaces.Services;
using BreakEven.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BreakEven.API.Controllers;

[Route("api/employees")]
[ApiController]
public class EmployeeController : ControllerBase
{
    // GET: api/employee 
    [HttpGet]
    public  IActionResult Get([FromServices] IEmployeeRepository repo)
    {
        try
        {
            var employees =  repo.GetAll();
            return Ok(employees);
        }
        catch (Exception e)
        { 
            Console.Write(e);
            return StatusCode(500);
        }
    }

    // GET: api/employee/:cpf
    [HttpGet("{cpf}")]
    public IActionResult GetByCpf(string cpf, [FromServices] IEmployeeRepository repo)
    {
        try
        {
            var employee =  repo.GetByCpf(cpf);
            if (employee == null)
                return NotFound(new { Error = "Employee not found" });
            return Ok(employee);
        }
        catch (Exception e)
        {
            Console.Write(e);
            return StatusCode(500);
        }
    }

    // GET: api/employee/:cpf/paycheck
    [HttpGet("{cpf}/paycheck")]
    public IActionResult GetPaycheck(string cpf, [FromServices] IEmployeeRepository repo, [FromServices] IPaycheckService paycheckService)
    {
        try
        {
            var employee =  repo.GetByCpf(cpf);
            if (employee == null)
                return NotFound(new { Error = "Employee not found" });

            var paycheck = paycheckService.GeneratePaycheck(employee);
               
            var result = PaycheckViewModel.FromDomain(paycheck);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.Write(e);
            return StatusCode(500);
        }
    }
        
    // POST api/employee
    [HttpPost]
    public  IActionResult  Post([FromBody] CreateEmployeeViewModel employeeViewModel, [FromServices] IEmployeeRepository repo)
    {
        try
        {
            var validator = new EmployeeValidator();
            var validationResult = validator.Validate(employeeViewModel);
            if (!validationResult.IsValid)
                return BadRequest(new { Error = validationResult.Errors[0].ErrorMessage });

            var dbEmployee = repo.GetByCpf(employeeViewModel.CPF);
            if (dbEmployee != null)
                return BadRequest(new { Error = "User already exists" });

            var employee = employeeViewModel.ToDomain();
            repo.Create(employee);

            return Created(nameof(Get), new { Id = employee.CPF });
        }
        catch (NegativeSalaryException e)
        {
            return BadRequest(new { Error = e.Message });            
        }
        catch (Exception e)
        {
            Console.Write(e);
            return StatusCode(500);
        }
    }
}
