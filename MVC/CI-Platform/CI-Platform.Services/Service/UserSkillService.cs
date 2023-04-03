using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service
{
    public class UserSkillService:IUserSkillService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserSkillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public static UserSkillVM ConvertUserSkillToVM(UserSkill userSkill)
        {
            return new UserSkillVM()
            {
                SkillId = userSkill.SkillId,
                UserId = userSkill.UserId,
                UserSkillId = userSkill.UserSkillId,
                Skill = SkillService.ConvertSkillToVM(userSkill.Skill)
            };
        }

        public List<UserSkillVM> GetAll()
        {
            IEnumerable<UserSkill> userSkills = _unitOfWork.UserSkill.GetAll();
            if (userSkills == null) return null!;
            return userSkills.Select(x => ConvertUserSkillToVM(x) ).ToList();
        }

        public List<UserSkillVM> GetUserSkillsByUserId(long userId)
        {
            List<UserSkill> userSkills = _unitOfWork.UserSkill.GetAll()
                .Where(us => us.UserId == userId).ToList();
            if (userSkills.Count() == 0) return new();
            return userSkills.Select(us => ConvertUserSkillToVM(us)).ToList();
        }

        public void RemoveAllUserSkills(long userId)
        {
            var userSkills = _unitOfWork.UserSkill.GetAll()?.Where(us => us.UserId == userId);
            if(userSkills?.Count() > 0)
            {
                _unitOfWork.UserSkill.RemoveRange(userSkills);
            }
        }
        public void SaveAllUserSkills(List<short> skillIds, long userId)
        {
            
            IEnumerable<UserSkill> us = skillIds.Select(skillId =>
                new UserSkill()
                {
                    SkillId = skillId,
                    UserId = userId,
                    CreatedAt = DateTimeOffset.Now
                }
            );
            _unitOfWork.UserSkill.AddRange(us);
        }
    }
}
