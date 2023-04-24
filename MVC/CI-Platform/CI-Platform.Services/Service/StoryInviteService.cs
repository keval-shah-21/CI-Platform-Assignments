using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service
{
    public class StoryInviteService : IStoryInviteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public StoryInviteService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public static StoryInviteVM ConvertStoryInviteToVM(StoryInvite si)
        {
            return new StoryInviteVM()
            {
                FromUserId = si.FromUserId,
                StoryId = si.StoryId,
                StoryInviteId = si.StoryInviteId,
                ToUserId = si.ToUserId
            };
        }

        public void RemoveByStoryId(long id)
        {
            IEnumerable<StoryInvite> si = _unitOfWork.StoryInvite.GetAll().Where(s => s.StoryId == id);
            _unitOfWork.StoryInvite.RemoveRange(si);
        }
        public void RecommendStory(long storyId, long userId, long[] toUsers, string url)
        {
            User user = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == userId);
            string name = user.FirstName + " " + user.LastName;
            string subject = "CI Platform - Story Recommendation";
            string link = $"<a href='{url}' style='text-decoration:none;display:block;width:max-content;border:1px solid black;border-radius:5rem;padding:0.75rem 1rem;margin:1rem auto;color:black;font-size:1rem;'>Open story</a>";
            string body = $"<p style='text-align:center;font-size:2rem'>Your co-worker '{name}' has recommended a story to you.</p><p style='text-align:center;font-size:1.5rem'>Click on the link below check their story out.</p><hr/>{link}";

            foreach (long toUser in toUsers)
            {
                _unitOfWork.StoryInvite.Add(new StoryInvite
                {
                    CreatedAt = DateTimeOffset.Now,
                    FromUserId = userId,
                    ToUserId = toUser,
                    StoryId = storyId,
                });
                string email = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == toUser).Email;
                _ = _emailService.SendEmail(email, subject, body);
            }
        }
    }
}
