using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Services.Service.Interface
{
    public interface IMissionInviteService
    {
        void RecommendMission(long missionId, long userId, long[] toUsers, string url);
    }
}
