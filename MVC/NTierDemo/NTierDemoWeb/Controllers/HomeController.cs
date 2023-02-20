using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NTierDemoWeb.Models;
using NTierDemo.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using NTierDemo.Repositories.Interface;

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

    [Route("", Name="Default")]
    [Route("home/employee-list", Name="EmployeeList")]
    public async Task<IActionResult> Index()
    {
        List<EmployeeModel> employees = await _employeeRepository.GetAllEmployeesAsync();
        return View(employees);
    }

    [Route("home/employee-list-json", Name="EmployeeListJson")]
    public async Task<IActionResult> EmployeeListJson()
    {
        List<EmployeeModel> employees = await _employeeRepository.GetAllEmployeesAsync();
        return Json(new {data = employees});
    }

    [Route("home/create-employee", Name="CreateEmployee")]
    public IActionResult CreateEmployee(){
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    [Route("home/create-employee", Name="CreateEmployeePost")]
    public async Task<IActionResult> CreateEmployee(EmployeeModel model){
        if(ModelState.IsValid){
            await _employeeRepository.AddEmployeeAsync(model);
            return RedirectToRoute("EmployeeList");
        }
        ViewData["ModelState"] = "Model state invalid.";
        return View(model);
    }

    [Route("home/edit-employee", Name="EditEmployee")]
    public async Task<IActionResult> EditEmployee(int? Id){
        if(Id == null || Id == 0) return NotFound();
        EmployeeModel model = await _employeeRepository.GetEmployeeByIdAsync(Id);
        if(model == null) return NotFound();
        return View(model);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    [Route("home/edit-employee", Name="EditEmployeePost")]
    public async Task<IActionResult> EditEmployee(EmployeeModel model){
        if(ModelState.IsValid){
            await _employeeRepository.UpdateEmployeeAsync(model);
            return RedirectToRoute("EmployeeList");
        }
        ViewData["ModelState"] = "Model state invalid.";
        return View(model);
    }

    [Route("home/delete-employee", Name="DeleteEmployee")]
    public async Task<IActionResult> DeleteEmployee(int? Id){
        if(Id == null || Id == 0) return NotFound();
        EmployeeModel model = await _employeeRepository.GetEmployeeByIdAsync(Id);
        if(model == null) return NotFound();
        return View(model);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    [Route("home/delete-employee", Name="DeleteEmployeePost")]
    public async Task<IActionResult> DeleteEmployee(EmployeeModel model){
        Console.WriteLine(model.FirstName);
        Console.WriteLine(ModelState.IsValid);
        if(ModelState.IsValid){
            await _employeeRepository.DeleteEmployeeAsync(model);
            return RedirectToRoute("EmployeeList");
        }
        ViewData["ModelState"] = "Model state invalid.";
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
