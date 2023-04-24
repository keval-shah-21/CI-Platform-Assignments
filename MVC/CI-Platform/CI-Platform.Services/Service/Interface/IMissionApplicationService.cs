using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionApplicationService
{
    void CancelMission(long missionId, long userId);
    List<MissionVM> GetAllUserMissions(long userId);
    void UpdateStatus(long id, byte value);
    MissionApplicationVM GetById(long id);
    List<MissionApplicationVM> GetAllAdmin();
    void ApplyMission(long missionId, long userId);
    List<MissionApplicationVM> GetAllWithInclude();
    List<MissionApplicationVM> Search(string? query);
}
