using CI_Platform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Services.Service.Interface
{
    public interface IStoryService
    {
        void SaveStory(StoryVM storyVM, long userId, byte approvalStatus);
        void UpdateStory(StoryVM storyVM, byte approvalStatus);
        StoryVM GetDraftStoryByUserId(long userId);
        List<StoryVM> GetAll();
        long GetLatestStoryId(long userId);
        void RemoveStoryById(long storyId);
        StoryVM GetStoryById(long? id);
        void UpdateTotalViews(long storyId, long totalViews);
        List<StoryVM> GetAdminStories();
        void AcceptStory(long id);
        void UpdateStatus(long id, byte value);
        void DeleteStory(long id);
        List<StoryVM> Search(string? query);
    }
}
