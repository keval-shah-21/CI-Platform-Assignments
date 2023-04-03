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
                ShortDescription = story.ShortDescription,
                Description = story.Description,
                MissionId = story.MissionId,
                MissionTheme = story.Mission.MissionTheme.MissionThemeName,
                UserId = story.UserId,
                PublishedAt = story.PublishedAt,
                Title = story.Title,
                VideoUrl = story.VideoUrl,
                TotalViews = story.TotalViews,
                StoryThumbnail = GetStoryThumbnail(story),
                StoryMediaVM = GetStoryMedia(story),
                UserVM = GetStoryUser(story)
            };
        }
        public List<StoryVM> GetAll()
        {
            IEnumerable<Story> stories = _unitOfWork.Story.GetAllWithInclude();
            if (stories.LongCount() == 0) return new();
            return stories.Select(s => ConvertStoryToVM(s)).ToList();
        }
        public void SaveStory(StoryVM storyVM, long userId, byte approvalStatus)
        {
            _unitOfWork.Story.Add(new Story()
            {
                MissionId = storyVM.MissionId,
                UserId = userId,
                ShortDescription = storyVM.ShortDescription,
                Description = storyVM.Description,
                Title = storyVM.Title,
                ApprovalStatus = approvalStatus,
                CreatedAt = DateTimeOffset.Now,
                VideoUrl = storyVM?.VideoUrl
            });
        }

        public void UpdateStory(StoryVM storyVM, byte approvalStatus)
        {
            Story story = _unitOfWork.Story.GetFirstOrDefault(s => s.StoryId == storyVM.StoryId);
            story.MissionId = storyVM.MissionId;
            story.Description = storyVM.Description;
            story.ShortDescription = storyVM.ShortDescription;
            story.Title = storyVM.Title;
            story.ApprovalStatus = approvalStatus;
            story.VideoUrl = storyVM?.VideoUrl;
            story.UpdatedAt = DateTimeOffset.Now;
            _unitOfWork.Story.Update(story);
        }
        public StoryVM GetStoryById(long? id)
        {
            Story story = _unitOfWork.Story.GetFirstOrDefaultWithInclude(s => s.StoryId == id);
            if (story == null) return null!;
            return ConvertStoryToVM(story);
        }
        public StoryVM GetDraftStoryByUserId(long userId)
        {
            Story s = _unitOfWork.Story.GetFirstOrDefaultWithInclude(s => s.UserId == userId && s.ApprovalStatus == 3);
            if(s == null) return new();
            return ConvertStoryToVM(s);
        }
        public void RemoveStoryById(long storyId)
        {
            _unitOfWork.Story.Remove(_unitOfWork.Story.GetFirstOrDefault(s => s.StoryId == storyId));
        }
        public long GetLatestStoryId(long userId)
        {
            return _unitOfWork.Story.GetAll()
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedAt).FirstOrDefault()!.StoryId;
        }
        public void UpdateTotalViews(long storyId, long totalViews)
        {
            _unitOfWork.Story.UpdateTotalViews(storyId, totalViews);
        }
        internal static string GetStoryThumbnail(Story story)
        {
            StoryMedium sm = story.StoryMedia.FirstOrDefault();
            return sm?.MediaPath + sm?.MediaName + sm?.MediaType;
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
