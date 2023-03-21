using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.DataAccess.Repository
{
    public class MissionInviteRepository : Repository<MissionInvite>, IMissionInviteRepository
    {
        public MissionInviteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
