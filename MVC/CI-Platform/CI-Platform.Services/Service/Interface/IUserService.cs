using CI_Platform.Entities.ViewModels;
using CI_Platform.Entities.DataModels;
namespace CI_Platform.Services.Service.Interface;

public interface IUserService
{
    void Add(UserVM User);

    UserVM Login(LoginVM Login);

    UserVM GetFirstOrDefaultByEmail(string Email);
}