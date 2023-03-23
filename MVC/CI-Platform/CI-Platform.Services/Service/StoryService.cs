using CI_Platform.Entities.Constants;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Services.Service
{
    public class StoryService:IStoryService
    {

        public static StoryVM ConvertStoryToVM(Story story)
        {
            return new StoryVM
            {
                StoryId = story.StoryId,
                ApprovalStatus = story.ApprovalStatus == 0 ? ApprovalStatus.PENDING :
                story.ApprovalStatus == 1 ? ApprovalStatus.APPROVED :
                story.ApprovalStatus == 2 ? ApprovalStatus.DECLINED :
                ApprovalStatus.DRAFT,
                CreatedAt = story.CreatedAt,
                Description = story.Description,
                MissionId = story.MissionId,
                UserId = story.UserId,
                PublishedAt = story.PublishedAt,
                Title = story.Title
            };
        }
    }
}
