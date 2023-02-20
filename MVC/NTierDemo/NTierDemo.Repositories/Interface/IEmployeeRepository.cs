using NTierDemo.Models.ViewModels;
namespace NTierDemo.Repositories.Interface;

public interface IEmployeeRepository
{
    Task<List<EmployeeModel>> GetAllEmployeesAsync();

    Task<int> AddEmployeeAsync(EmployeeModel model);

    Task<EmployeeModel> GetEmployeeByIdAsync(int? Id);

    Task<int> UpdateEmployeeAsync(EmployeeModel model);

    Task<int> DeleteEmployeeAsync(EmployeeModel model);
}