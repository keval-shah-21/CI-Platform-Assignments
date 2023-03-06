using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionThemeService
{
    List<MissionThemeVM> GetAll();
}
