using CI_Platform.Entities.ViewModels;
namespace CI_Platform.Services.Service.Interface;

public interface IUserService
{
    void Add(UserVM userVM);

    UserVM Login(LoginVM loginVM);

    UserVM GetFirstOrDefaultByEmail(string email);

    void SendResetPasswordEmail(string email, string url);

    void UpdateByPassword(string email, string password);
}