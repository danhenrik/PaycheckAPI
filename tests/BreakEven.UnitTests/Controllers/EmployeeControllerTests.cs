namespace BreakEven.UnitTests.Controllers;

using BreakEven.API.Interfaces.Services;
using BreakEven.API.Controllers;
using BreakEven.API.Entities;
using BreakEven.API.Interfaces.Repositories;
using BreakEven.API.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class EmployeeControllerTests
{
    private readonly EmployeeController _controller = new();
    
    private readonly Mock<IEmployeeRepository> _repoMock = new();

    private readonly CreateEmployeeViewModel _employeeViewModel = new()
    {
        CPF = "12345678900",
        FirstName = "Test",
        SurName = "Employee",
        GrossSalary = 2000,
        Sector = "Test",
        AdmissionDate = DateTime.Now,
        HasDentalInsurance = true,
        HasHealthInsurance = true,
        HasTransportationAllowance = true
    };
    
    private readonly Employee _employee = new()
    {
        CPF = "12345678900",
        FirstName = "Test",
        SurName = "Employee",
        GrossSalary = 2000,
        Sector = "Test",
        AdmissionDate = DateTime.Now,
        HasDentalInsurance = false,
        HasHealthInsurance = false,
        HasTransportationAllowance = false
    };
    
    [Fact]
    public void Get_IfEmployeesAreFound_ReturnsEmployeeList()
    {
        var employees = new List<Employee> { _employee };
        _repoMock
            .Setup(repo => repo.GetAll())
            .Returns(employees);

        var  result = _controller.Get(_repoMock.Object);
            
        var response = Assert.IsType<OkObjectResult>(result);
        var employeesRes = Assert.IsType<List<Employee>>(response.Value);
        Assert.Equal(1, employeesRes.Count);
    }
    
    [Fact]
    public void Get_IfNoEmployeesAreFound_ReturnsEmptyList()
    {
        var employees = new List<Employee>();
        _repoMock
            .Setup(repo => repo.GetAll())
            .Returns(employees);

        var  result = _controller.Get(_repoMock.Object);
            
        var response = Assert.IsType<OkObjectResult>(result);
        var employeesRes = Assert.IsType<List<Employee>>(response.Value);
        Assert.Equal(0, employeesRes.Count);
    }
    
    [Fact] 
    public void Get_IfExceptionIsThrown_ReturnsError()
    {
        _repoMock
            .Setup(repo => repo.GetAll())
            .Throws(new Exception());

        var  result = _controller.Get(_repoMock.Object);
            
        Assert.IsType<StatusCodeResult>(result);
    }
    
    [Fact]
    public void GetById_IfEmployeeFound_ReturnsEmployee()
    {

        _repoMock
            .Setup(repo => repo.GetByCpf(It.IsAny<string>()))
            .Returns(_employee);

        var  result = _controller.GetByCpf("cpf", _repoMock.Object);
            
        var response = Assert.IsType<OkObjectResult>(result);
        var employeesRes = Assert.IsType<Employee>(response.Value);
        Assert.Equal(_employee, employeesRes);
    }
    
    [Fact]
    public void GetById_IfEmployeeNotFound_ReturnsError()
    {
        _repoMock
            .Setup(repo => repo.GetByCpf(It.IsAny<string>()))
            .Returns((Employee)null!);

        var  result = _controller.GetByCpf("cpf", _repoMock.Object);
            
        Assert.IsType<NotFoundObjectResult>(result);
    }
    
    [Fact]
    public void GetById_IfExceptionIsThrown_ReturnsError()
    {
        _repoMock
            .Setup(repo => repo.GetByCpf(It.IsAny<string>()))
            .Throws(new Exception());

        var  result = _controller.GetByCpf("cpf", _repoMock.Object);
            
        Assert.IsType<StatusCodeResult>(result);
    }
    
    [Fact]
    public void Post_IfEmployeeIsCreated_ReturnsOkWithId()
    {
        _repoMock
            .Setup(repo => repo.GetByCpf(It.IsAny<string>()))
            .Returns((Employee)null!);
        _repoMock
            .Setup(repo => repo.Create(It.IsAny<Employee>()))
            .Returns(Task.CompletedTask);
        
        var  result = _controller.Post(_employeeViewModel, _repoMock.Object);
            
        var obj = Assert.IsType<CreatedResult>(result).Value;
        var id = obj
            .GetType()
            .GetProperty("Id")
            .GetValue(obj);
        Assert.Equal(_employeeViewModel.CPF, id);
    }

    [Fact]
    public void Post_IfPayloadIsInvalid_ReturnsError()
    {
        var  result = _controller.Post(new CreateEmployeeViewModel() {CPF = "abcdefghijkl"}, _repoMock.Object);
            
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public void Post_IfUserAlreadyExits_ReturnsError()
    {
        _repoMock
            .Setup(repo => repo.GetByCpf(It.IsAny<string>()))
            .Returns(_employee);

        var  result = _controller.Post(_employeeViewModel, _repoMock.Object);
            
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    [Fact]
    public void Post_IfExceptionIsThrown_ReturnsError()
    {
        _repoMock
            .Setup(repo => repo.GetByCpf(It.IsAny<string>()))
            .Throws(new Exception());

        var  result = _controller.Post(_employeeViewModel, _repoMock.Object);
            
        Assert.IsType<StatusCodeResult>(result);
    }
    
    [Fact]
    public void GetPaycheck_IfEmployeeFound_ReturnsPaycheck()
    {
        _repoMock
            .Setup(repo => repo.GetByCpf(It.IsAny<string>()))
            .Returns(_employee);
        var paycheck = new Paycheck { GrossSalary = 1000 };
        var paycheckServiceMock = new Mock<IPaycheckService>();
        paycheckServiceMock
            .Setup(x => x.GeneratePaycheck(It.IsAny<Employee>()))
            .Returns(paycheck);
        
        var result = _controller.GetPaycheck("cpf", _repoMock.Object, paycheckServiceMock.Object);

        var response = Assert.IsType<OkObjectResult>(result);
        var paycheckRes = Assert.IsType<PaycheckViewModel>(response.Value);
        Assert.Equivalent(paycheckRes, PaycheckViewModel.FromDomain(paycheck));
    }
    
    [Fact]
    public void GetPaycheck_IfEmployeeNotFound_ReturnsError()
    {
        _repoMock
            .Setup(repo => repo.GetByCpf(It.IsAny<string>()))
            .Returns((Employee)null!);
        var paycheckServiceMock = new Mock<IPaycheckService>();

        var  result = _controller.GetPaycheck("cpf", _repoMock.Object, paycheckServiceMock.Object);
            
        Assert.IsType<NotFoundObjectResult>(result);
    }
    
    [Fact]
    public void GetPaycheck_IfExceptionIsThrown_ReturnsError()
    {
        _repoMock
            .Setup(repo => repo.GetByCpf(It.IsAny<string>()))
            .Throws(new Exception());
        var paycheckServiceMock = new Mock<IPaycheckService>();

        var result = _controller.GetPaycheck("cpf", _repoMock.Object, paycheckServiceMock.Object);
            
        Assert.IsType<StatusCodeResult>(result);
    }
}
