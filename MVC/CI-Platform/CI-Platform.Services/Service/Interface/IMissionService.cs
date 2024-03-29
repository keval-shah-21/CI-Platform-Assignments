using CI_Platform.Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionService
{
    List<IndexMissionVM> GetAllIndexMissions();
    IEnumerable<AdminMissionVM> GetAllAdminMission();

    List<IndexMissionVM> FilterMissions(int[]? country, int[]? city, int[]? theme, int[]? skill, string? search, int? sort, long? userId);

    MissionVM GetMissionById(long? id);
    Task<string> GetMissionNameById(long id);

    MissionVM UpdateMissionRating(long id);

    List<IndexMissionVM> GetRelatedMissions(long id);

    void UpdateActiveStatus(long id, int value);

    Task<long> AddTimeMission(TimeMissionVM time, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills, string wwwRootPath);
    Task<long> AddGoalMission(GoalMissionVM goal, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills, string wwwRootPath);
    
    List<AdminMissionVM> Search(string? query);
    TimeMissionVM GetTimeMissionById(long id);
    GoalMissionVM GetGoalMissionById(long id);
    Task UpdateTimeMission(TimeMissionVM time, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills, List<string> preLoadedImages, List<string> preLoadedDocs, List<string> preLoadedSkills, string wwwRootPath);
    Task UpdateGoalMission(GoalMissionVM goal, List<IFormFile> ImagesInput, List<IFormFile> DocumentsInput, List<string> MissionSkills, List<string> preLoadedImages, List<string> preLoadedDocs, List<string> preLoadedSkills, string wwwRootPath);
    void CloseMission(long id);
}
