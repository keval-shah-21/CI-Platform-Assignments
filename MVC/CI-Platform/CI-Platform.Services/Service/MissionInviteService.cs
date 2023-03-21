using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service
{
    public class MissionInviteService : IMissionInviteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public MissionInviteService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public static MissionInviteVM ConvertMissionInviteToVM(MissionInvite mi)
        {
            return new MissionInviteVM()
            {
                FromUserId = mi.FromUserId,
                MissionId = mi.MissionId,
                MissionInviteId = mi.MissionInviteId,
                ToUserId = mi.ToUserId
            };
        }

        public async void RecommendMission(long missionId, long userId, long[] toUsers, string url)
        {
            User user = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == userId);
            string name = user.FirstName + " " + user.LastName;
            string subject = "CI Platform - Mission Recommendation";
            string link = $"<a href='{url}' style='text-decoration:none;display:block;width:max-content;border:1px solid black;border-radius:5rem;padding:0.75rem 1rem;margin:1rem auto;color:black;font-size:1rem;'>Open mission</a>";
            string body = $"<p style='text-align:center;font-size:2rem'>Your co-worker {name} has recommended a mission to you.</p><p style='text-align:center;font-size:1.5rem'>Click on the link below check mission out</p><hr/>{link}";

            foreach (long toUser in toUsers)
            {
                _unitOfWork.MissionInvite.Add(new MissionInvite
                {
                    CreatedAt = DateTimeOffset.Now,
                    FromUserId = userId,
                    ToUserId = toUser,
                    MissionId = missionId,
                });
                string email = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == toUser).Email;
                _ = _emailService.SendEmail(email, subject, body);
            }
        }
    }
}
