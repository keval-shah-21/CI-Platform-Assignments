using NTierDemoEntity.ViewModels;
using NTierDemoEntity.DataModels;
using NTierDemoRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace NTierDemoRepository;

public class EmployeeRepository: IEmployeeRepository
{
    private readonly EmployeeDbContext _context;

    public EmployeeRepository(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeModel>> GetEmployeesAsync(){
        return await _context.Employees.Select(employee => new EmployeeModel(){
            Id = employee.Id,
            FirstName = employee.FirstName,
            Role = employee.Role,
        }).ToListAsync();
    }

    public async Task<int> AddEmployeeAsync(EmployeeModel model){
        Employee employee = new Employee(){
            Id = model.Id,
            FirstName = model.FirstName,
            Role = model.Role,
        };
         _context.Employees.Add(employee);
         await _context.SaveChangesAsync();
        return employee.Id;
    }

    public async Task<int> EditEmployeeAsync(EmployeeModel model){
        Employee employee = new Employee(){
            Id = model.Id,
            FirstName = model.FirstName,
            Role = model.Role,
        };
         _context.Employees.Update(employee);
         await _context.SaveChangesAsync();
        return employee.Id;
    }

    // public async Task<EmployeeModel> GetEmployeeById(int Id){
    //     return await _context.Employees.Find(Id);
    // }
}
