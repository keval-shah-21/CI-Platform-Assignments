using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
    public void DeleteById(long timesheetId)
    {
        var timesheetIdaram = new SqlParameter("@timesheetId", timesheetId);

        _context.Database.ExecuteSqlRaw("DELETE mission_timesheet WHERE timesheet_id = @timesheetId", timesheetIdaram);
    }
}
