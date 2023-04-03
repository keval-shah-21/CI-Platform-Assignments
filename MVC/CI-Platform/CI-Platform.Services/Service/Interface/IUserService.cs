using CI_Platform.Entities.ViewModels;
namespace CI_Platform.Services.Service.Interface;

public interface IUserService
{
    List<UserVM> GetAll();
    void Add(UserVM userVM);

    UserVM Login(LoginVM loginVM);

    UserVM GetFirstOrDefaultByEmail(string email);

    void SendResetPasswordEmail(string email, string url);

    void UpdatePassword(string email, string password);

    List<UserVM> GetAllUsersToRecommendMission();
    List<UserVM> GetAllUsersToRecommendStory();

    ProfileVM GetUserProfileById(long userId);
    void UpdateUserProfile(ProfileVM profileVM);

    bool IsPasswordValid(string email, string password);
}