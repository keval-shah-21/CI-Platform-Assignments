using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IMissionThemeService
{
    List<MissionThemeVM> GetAll();
    MissionThemeVM GetThemeById(long id);
    void SaveTheme(MissionThemeVM mt);
    void UpdateTheme(MissionThemeVM mt);
    bool IsAlreadyUsed(long id);
    void UpdateStatusByid(long id, int value);
    void DeleteTheme(long id);
    List<MissionThemeVM> Search(string? query);
    bool IsThemeUnique(string themeName, long? id);
}
