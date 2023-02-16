using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NTierDemoWeb.Models;
using NTierDemoEntity.ViewModels;
using NTierDemoRepository.Interface;

namespace NTierDemoWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmployeeRepository _employeeRepository;

    public HomeController(ILogger<HomeController> logger, IEmployeeRepository employeeRepository)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
    }

    [Route("", Name = "Default")]
    [Route("home/employee-list", Name="EmployeeList")]
    public async Task<IActionResult> Index()
    {
        IEnumerable<EmployeeModel> employees = await _employeeRepository.GetEmployeesAsync();
        return View(employees);
    }

    [Route("home/employee-list-json", Name="EmployeeListJson")]
    public async Task<IActionResult> EmployeeListJson(){
        IEnumerable<EmployeeModel> employees = await _employeeRepository.GetEmployeesAsync();
        return Json(new {data= employees});
    }

    [Route("home/create-employee", Name="CreateEmployee")]
    public IActionResult Create(){
        return View();
    }

    [Route("home/create-employee", Name="CreateEmployee")]
    public async Task<IActionResult> Create(EmployeeModel model){
        if(ModelState.IsValid){
            await _employeeRepository.AddEmployeeAsync(model);
            return RedirectToAction("EmployeeList");
        }
        ViewData["ModelState"] = "Model state invalid";
        return View(model);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
