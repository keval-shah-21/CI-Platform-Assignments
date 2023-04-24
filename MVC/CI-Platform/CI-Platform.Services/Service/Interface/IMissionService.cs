using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionService
{
    List<IndexMissionVM> GetAllIndexMissions();
    IEnumerable<AdminMissionVM> GetAllAdminMission();

    List<CountryVM> GetCountriesByMissions(List<IndexMissionVM> missionVM);

    List<CityVM> GetCitiesByMissions(List<IndexMissionVM> missionVM);

    List<IndexMissionVM> FilterMissions(int[]? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, long? userId);

    MissionVM GetMissionById(long? id);

    MissionVM UpdateMissionRating(long id);

    List<IndexMissionVM> GetRelatedMissions(long id);

    void UpdateStatus(long id, int value);

    long AddTimeMission(TimeMissionVM time);
    long AddGoalMission(GoalMissionVM time);
    List<AdminMissionVM> Search(string? query);
    TimeMissionVM GetTimeMissionById(long id);
    GoalMissionVM GetGoalMissionById(long id);
    void UpdateTimeMission(TimeMissionVM time);
    void UpdateGoalMission(GoalMissionVM time);
}
