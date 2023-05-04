using CI_Platform.Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CI_Platform.Services.Service.Interface;

public interface IUserService
{
    List<UserVM> GetAll();
    long Add(UserVM userVM);

    UserVM Login(LoginVM loginVM);
    AdminVM AdminLogin(LoginVM loginVM);

    UserVM GetFirstOrDefaultByEmail(string email);
    UserAdminVM GetFirstOrDefaultUserAdmin(long id);

    void SendResetPasswordEmail(string email, string url);
    void SendAccountCreatedMail(string email, string password, string url);

    void UpdatePassword(string email, string password);

    List<UserVM> GetAllUsersToRecommendMission();
    List<UserVM> GetAllUsersToRecommendStory();

    ProfileVM GetUserProfileById(long userId);
    void UpdateUserProfile(ProfileVM profileVM);

    bool IsPasswordValid(string email, string password);

    void SaveVerifyAccountDetails(string email, string token);
    void SendVerifyAccountEmail(string email, string url);

    bool VerifyEmail(string email, string token);
    void ActivateUserByEmail(string email);
    void DeactivateUserByEmail(string email);
    void RemoveVerifyEmail(string email);

    List<UserVM> SearchUser(string? query);
    bool IsProfileFilled(long id);
    void UpdateUserAdmin(UserAdminVM userAdmin);
    void AddUserAdmin(UserAdminVM user);

    string SaveProfileImage(string wwwRootPath, IFormFile profileInput);
    public void RemoveProfileImage(string wwwRootPath, string preloadedImage);
}