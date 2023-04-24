using CI_Platform.Entities.DataModels;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IMissionRepository: IRepository<Mission>
{
    void UpdateStatus(long id, int value);
    Mission GetFirstOrDefaultWithInclude(Expression<Func<Mission, bool>> filter);
}
