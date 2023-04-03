using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.DataAccess.Repository;

public class MissionTimesheetRepository : Repository<MissionTimesheet>, IMissionTimesheetRepository
{
    public MissionTimesheetRepository(ApplicationDbContext context) : base(context)
    {
    }
    public IEnumerable<MissionTimesheet> GetAllWithInclude()
    {
        return dbSet
            .Include(mt => mt.Mission)
            .ToList();
    }
}
