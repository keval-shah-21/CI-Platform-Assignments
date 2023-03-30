using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.DataAccess.Repository
{
    public class UserSkillRepository : Repository<UserSkill>, IUserSkillRepository
    {
        public UserSkillRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<UserSkill> GetAll()
        {
            return dbSet
                .Include(us => us.Skill)
                .ToList();
        }
    }
}
