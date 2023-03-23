using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionApplicationService
{
    List<MissionApplicationVM> GetAll();
    List<MissionVM> GetAllUserMissions(long userId);
}
