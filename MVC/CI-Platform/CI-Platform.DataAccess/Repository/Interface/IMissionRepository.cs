using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IMissionRepository: IRepository<Mission>
{
    List<Mission> GetAllMission();
}
