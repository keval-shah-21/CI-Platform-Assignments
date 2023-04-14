using CI_Platform.Entities.DataModels;

namespace CI_Platform.DataAccess.Repository.Interface;

public interface IMissionTimesheetRepository: IRepository<MissionTimesheet>
{
    IEnumerable<MissionTimesheet> GetAllWithInclude();
    void DeleteById(long timesheetId);
    void UpdateStatus(long id, int value);
}
