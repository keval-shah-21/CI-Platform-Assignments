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

        public async Task RecommendMission(long missionId, long userId, long[] toUsers, string url)
        {
            User user = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == userId);
            string name = user.FirstName + " " + user.LastName;
            string subject = "CI Platform - Mission Recommendation";
            string link = $"<a href='{url}' style='text-decoration:none;display:block;width:max-content;border:1px solid black;border-radius:5rem;padding:0.75rem 1rem;margin:1rem auto;color:black;font-size:1rem;'>Open mission</a>";
            string body = $"<p style='text-align:center;font-size:2rem'>Your co-worker '{name}' has recommended a mission to you.</p><p style='text-align:center;font-size:1.5rem'>Click on the link below check mission out.</p><hr/>{link}";

            foreach (long toUser in toUsers)
            {
                NotificationSetting setting = await _unitOfWork.NotificationSetting.GetFirstOrDefaultAsync(n => n.UserId == toUser);
                if (setting.Email == false) continue;

                await _unitOfWork.MissionInvite.AddAsync(new MissionInvite
                {
                    CreatedAt = DateTimeOffset.Now,
                    FromUserId = userId,
                    ToUserId = toUser,
                    MissionId = missionId,
                });
                var newUser = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.UserId == toUser);
                _ = _emailService.SendEmailAsync(newUser.Email, subject, body);
            }
        }
    }
}
