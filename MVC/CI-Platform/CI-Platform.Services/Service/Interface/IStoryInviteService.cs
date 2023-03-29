using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Services.Service.Interface
{
    public interface IStoryInviteService
    {
        void RecommendStory(long storyId, long userId, long[] toUsers, string url);
    }
}
