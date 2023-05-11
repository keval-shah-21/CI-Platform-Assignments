using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IUserRepository : IRepository<User>
{
    List<User> GetAllToRecommendMission();
    List<User> GetAllToRecommendStory();
    void UpdatePassword(string email, string password);
    Task UpdateIsBlockedAsync(string email, int value);
    Task UpdateStatusAsync(string email, int value);
}
