using CI_Platform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Services.Service.Interface
{
    public interface IStoryMediaService
    {
        void SaveAllStoryMedia(List<StoryMediaVM> storyMediaVM);
        void RemoveStoryMedia(long storyId, string mediaName);
        void RemoveAllStoryMediaByStoryId(long storyId);
        void RemoveMediaFromFolder(long storyId, string wwwRootPath);
    }
}
