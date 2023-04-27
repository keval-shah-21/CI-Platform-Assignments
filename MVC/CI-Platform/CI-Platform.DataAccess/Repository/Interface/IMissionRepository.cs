using CI_Platform.Entities.DataModels;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IMissionRepository: IRepository<Mission>
{
    void UpdateActiveStatus(long id, int value);
    IEnumerable<Mission> GetAllAdmin();
    Mission GetFirstOrDefaultWithInclude(Expression<Func<Mission, bool>> filter);
    Mission GetFirstOrDefaultAdminMission(Expression<Func<Mission, bool>> filter);
}
