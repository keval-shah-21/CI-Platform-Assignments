using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.Constants;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service
{
    public class StoryService:IStoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
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
                MissionTheme = story.Mission.MissionTheme.MissionThemeName,
                UserId = story.UserId,
                PublishedAt = story.PublishedAt,
                Title = story.Title,
                StoryMediaVM = GetStoryMedia(story),
                UserVM = GetStoryUser(story)
            };
        }
        public List<StoryVM> GetAll()
        {
            IEnumerable<Story> stories = _unitOfWork.Story.GetAll();
            if (stories.LongCount() == 0) return new();
            return stories.Select(s => ConvertStoryToVM(s)).ToList();
        }
        public void SaveStory(StoryVM storyVM, long userId, byte approvalStatus)
        {
            _unitOfWork.Story.Add(new Story()
            {
                MissionId = storyVM.MissionId,
                UserId = userId,
                Description = storyVM.Description,
                Title = storyVM.Title,
                ApprovalStatus = approvalStatus,
                CreatedAt = DateTimeOffset.Now,
                VideoUrl = storyVM.VideoUrl
            });
        }

        public void UpdateStory(StoryVM storyVM, byte approvalStatus)
        {
            Story story = _unitOfWork.Story.GetFirstOrDefault(s => s.StoryId == storyVM.StoryId);
            story.MissionId = storyVM.MissionId;
            story.Description = storyVM.Description;
            story.Title = storyVM.Title;
            story.ApprovalStatus = approvalStatus;
            story.VideoUrl = storyVM.VideoUrl;
            story.UpdatedAt = DateTimeOffset.Now;
            _unitOfWork.Story.Update(story);
        }

        public StoryVM GetDraftStoryByUserId(long userId)
        {
            Story s = _unitOfWork.Story.GetFirstOrDefault(s => s.UserId == userId && s.ApprovalStatus == 3);
            if(s == null) return new();
            return ConvertStoryToVM(s);
        }

        internal static List<StoryMediaVM> GetStoryMedia(Story story)
        {
            return story?.StoryMedia?.Select(sm => StoryMediaService.ConvertStoryMediaToVM(sm)).ToList()!;
        }
        internal static UserVM GetStoryUser(Story story)
        {
            return UserService.ConvertUserToVM(story.User);
        }
    }
}
