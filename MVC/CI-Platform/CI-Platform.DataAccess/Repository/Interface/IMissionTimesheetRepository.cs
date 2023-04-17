using CI_Platform.Entities.DataModels;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IMissionTimesheetRepository: IRepository<MissionTimesheet>
{
    IEnumerable<MissionTimesheet> GetAllWithInclude();
    void DeleteById(long timesheetId);
    void UpdateStatus(long id, int value);
    MissionTimesheet GetFirstOrDefaultWithInclude(Expression<Func<MissionTimesheet, bool>> filter);
}
