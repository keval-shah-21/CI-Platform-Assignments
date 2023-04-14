using CI_Platform.Entities.ViewModels;
namespace CI_Platform.Services.Service.Interface;

public interface IUserService
{
    List<UserVM> GetAll();
    void Add(UserVM userVM);

    UserVM Login(LoginVM loginVM);
    UserVM AdminLogin(LoginVM loginVM);

    UserVM GetFirstOrDefaultByEmail(string email);
    UserVM GetFirstOrDefaultAdminByEmail(string email);

    void SendResetPasswordEmail(string email, string url);

    void UpdatePassword(string email, string password);
    void UpdateAdminPassword(string email, string password);

    List<UserVM> GetAllUsersToRecommendMission();
    List<UserVM> GetAllUsersToRecommendStory();

    ProfileVM GetUserProfileById(long userId);
    ProfileVM GetAdminProfileById(long id);

    void UpdateUserProfile(ProfileVM profileVM);
    void UpdateAdminProfile(ProfileVM profileVM);

    bool IsPasswordValid(string email, string password);

    void SaveVerifyAccountDetails(string email, string token);
    void SendVerifyAccountEmail(string email, string url);

    bool VerifyEmail(string email, string token);
    void ActivateUserByEmail(string email);
    void DeactivateUserByEmail(string email);
    void RemoveVerifyEmail(string email);

    List<UserVM> SearchUser(string? query);
}