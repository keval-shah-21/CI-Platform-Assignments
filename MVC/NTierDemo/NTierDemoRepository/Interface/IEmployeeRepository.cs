using NTierDemoEntity.ViewModels;
namespace NTierDemoRepository.Interface;

public interface IEmployeeRepository
{
    Task<List<EmployeeModel>> GetEmployeesAsync();

    Task<int> AddEmployeeAsync(EmployeeModel model);
}
