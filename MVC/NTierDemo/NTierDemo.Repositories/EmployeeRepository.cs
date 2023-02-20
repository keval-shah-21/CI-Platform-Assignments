using NTierDemo.Models.DataModels;
using NTierDemo.Models.ViewModels;
using NTierDemo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace NTierDemo.Repositories;

public class EmployeeRepository: IEmployeeRepository
{
    private readonly EmployeeDbContext _context;
    public EmployeeRepository(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeModel>> GetAllEmployeesAsync(){
        return await _context.Employees.Select(employee => new EmployeeModel(){
            Id = employee.Id,
            FirstName = employee.FirstName,
            Role = employee.Role
        })
        .ToListAsync();
    }

    public async Task<int> AddEmployeeAsync(EmployeeModel model){
        Employee employee = new Employee(){
            Id = model.Id,
            FirstName = model.FirstName,
            Role = model.Role
        };
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return employee.Id;
    }

    public async Task<EmployeeModel> GetEmployeeByIdAsync(int? Id){
        Employee employee =  await _context.Employees.FirstOrDefaultAsync(u => u.Id == Id);
        return new EmployeeModel(){
            Id = employee.Id,
            FirstName = employee.FirstName,
            Role = employee.Role
        };
    }

    public async Task<int> UpdateEmployeeAsync(EmployeeModel model){
        Employee employee = new Employee(){
            Id = model.Id,
            FirstName = model.FirstName,
            Role = model.Role
        };
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();

        return employee.Id;
    }

    public async Task<int> DeleteEmployeeAsync(EmployeeModel model){
        Employee employee = new Employee(){
            Id = model.Id,
            FirstName = model.FirstName,
            Role = model.Role
        };
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return employee.Id;
    }
}