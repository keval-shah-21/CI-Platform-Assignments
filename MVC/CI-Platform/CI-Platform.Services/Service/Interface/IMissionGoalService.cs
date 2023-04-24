using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionGoalService
{
    List<MissionGoalVM> GetAll();
    void UpdateGoalAchieved(long id, int? value);
    void AddMissionGoal(MissionGoalVM mg);
    void UpdateMissionGoal(MissionGoalVM missionGoal);
}
