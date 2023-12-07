using BreakEven.API.Helpers;
using BreakEven.API.Interfaces.Repositories;
using BreakEven.API.Interfaces.Services;
using BreakEven.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BreakEven.API.Controllers;

[Route("api/employee")]
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

    // GET: api/employee/:id
    [HttpGet("{id}")]
    public IActionResult GetById(string id, [FromServices] IEmployeeRepository repo)
    {
        try
        {
            var employee =  repo.GetByCpf(id);
            if (employee == null)
                return NotFound("Employee not found");
            return Ok(employee);
        }
        catch (Exception e)
        {
            Console.Write(e);
            return StatusCode(500);
        }
    }

    // GET: api/employee/:id/paycheck
    [HttpGet("{id}/paycheck")]
    public IActionResult GetPaycheck(string id, [FromServices] IEmployeeRepository repo, [FromServices] IPaycheckService paycheckService)
    {
        try
        {
            var employee =  repo.GetByCpf(id);
            if (employee == null)
                return NotFound("Employee not found");

            var paycheck = paycheckService.GeneratePaycheck(employee);
               
            var result = PaycheckVm.FromDomain(paycheck);
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
    public  IActionResult  Post([FromBody] CreateEmployeeVm employeeVm, [FromServices] IEmployeeRepository repo)
    {
        try
        {
            var validator = new EmployeeValidator();
            var validationResult = validator.Validate(employeeVm);
            if (!validationResult.IsValid)
                return BadRequest(new { Error = validationResult.Errors[0].ErrorMessage });

            var dbEmployee = repo.GetByCpf(employeeVm.CPF);
            if (dbEmployee != null)
                return BadRequest(new { Error = "User already exists" });
                
            var employee = employeeVm.ToDomain();
            repo.Create(employee);
           
            return Created(nameof(Get),new {Id = employee.CPF});
        }
        catch (Exception e)
        {
            Console.Write(e);
            return StatusCode(500);
        }
    }
}
