using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IMissionThemeRepository: IRepository<MissionTheme>
{
    void RemoveById(long id);
    bool IsAlreadyUsed(long id);
}
