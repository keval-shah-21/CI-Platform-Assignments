using CI_Platform.Entities.DataModels;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IMissionApplicationRepository : IRepository<MissionApplication>
{
    IEnumerable<MissionApplication> GetAllForStoryMissions();
    void CancelMission(long missionId, long userId);
    IEnumerable<MissionApplication> GetAllWithInclude();
    void UpdateStatus(long id, byte value);
    MissionApplication GetFirstOrDefaultWithInclude(Expression<Func<MissionApplication, bool>> filter);
}
