using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionRatingService
{
    List<MissionRatingVM> GetAll();

    void RateMission(long missionId, long userId, byte rate);
}