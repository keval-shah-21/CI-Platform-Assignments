using CI_Platform.Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CI_Platform.Services.Service.Interface
{
    public interface IStoryMediaService
    {
        void SaveAllStoryMedia(List<StoryMediaVM> storyMediaVM);
        void RemoveStoryMedia(long storyId, string mediaName);
        void RemoveAllStoryMediaByStoryId(long storyId);
        void RemoveMediaFromFolder(long storyId, string wwwRootPath);
        void AddAllStoryMedia(string wwwRootPath, List<IFormFile> images, long storyId);
        void EditAllStoryMedia(string wwwRootPath, List<IFormFile> images, long storyId, List<string>? preLoaded);
    }
}
