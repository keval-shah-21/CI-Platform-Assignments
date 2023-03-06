using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionService
{
    List<MissionVM> GetAll();
}
