using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CI_Platform.DataAccess.Repository;

public class MissionTimesheetRepository : Repository<MissionTimesheet>, IMissionTimesheetRepository
{
    private readonly ApplicationDbContext _context;
    public MissionTimesheetRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public IEnumerable<MissionTimesheet> GetAllWithInclude()
    {
        return dbSet
            .Include(mt => mt.Mission)
            .Include(mt => mt.User)
            .ToList();
    }

    public MissionTimesheet GetFirstOrDefaultWithInclude(Expression<Func<MissionTimesheet, bool>> filter)
    {
        return
            dbSet.
            Include(mt => mt.Mission)
            .Include(mt => mt.User)
            .FirstOrDefault(filter)!;
    }
    public void DeleteById(long timesheetId)
    {
        var timesheetIdaram = new SqlParameter("@timesheetId", timesheetId);

        _context.Database.ExecuteSqlRaw("DELETE mission_timesheet WHERE timesheet_id = @timesheetId", timesheetIdaram);
    }

    public void UpdateStatus(long id, int value)
    {
        SqlParameter idParameter = new SqlParameter("@timesheetId", id);
        SqlParameter statusParameter = new SqlParameter("@status", value);

        _context.Database.ExecuteSqlRaw("UPDATE mission_timesheet SET approval_status = @status WHERE timesheet_id = @timesheetId", statusParameter, idParameter);
    }
}
