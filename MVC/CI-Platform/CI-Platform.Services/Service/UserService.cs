using CI_Platform.Services.Service.Interface;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Entities.DataModels;
using CI_Platform.DataAccess.Repository.Interface;
using System.Linq.Expressions;

namespace CI_Platform.Services.Service;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSkillService _userSkillService;
    private readonly ICountryService _countryService;
    private readonly ICityService _cityService;
    private readonly ISkillService _skillService;
    private readonly IEmailService _emailService;

    public UserService(IUnitOfWork unitOfWork, IEmailService emailService, ISkillService skillService, ICityService cityService, ICountryService countryService, IUserSkillService userSkillService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _cityService = cityService;
        _countryService = countryService;
        _skillService = skillService;
        _userSkillService = userSkillService;
    }

    public static UserVM ConvertUserToVM(User user)
    {
        return new UserVM()
        {
            UserId = user.UserId,
            Avatar = user.Avatar,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            WhyIVolunteer = user.WhyIVolunteer,
            ProfileText = user.ProfileText,
            PhoneNumber = user.PhoneNumber,
            MissionInviteFromVM = GetMissionInviteFrom(user),
            MissionInviteToVM = GetMissionInviteTo(user),
            StoryInviteFromVM = GetStoryInviteFrom(user),
            StoryInviteToVM = GetStoryInviteTo(user)
        };
    }
    public ProfileVM ConvertProfileToVM(User user)
    {
        return new ProfileVM()
        {
            UserId = user.UserId,
            Avatar = user.Avatar,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            WhyIVolunteer = user.WhyIVolunteer,
            ProfileText = user.ProfileText,
            PhoneNumber = user.PhoneNumber,
            Department = user.Department,
            LinkedInUrl = user.LinkedInUrl,
            EmployeeId = user.EmployeeId,
            Title = user.Title,
            Availability = user.Availability,
            CityId = user.CityId,
            CountryId = user.CountryId,
            skillVMs = _skillService.GetAll(),
            cityVMs = _cityService.GetAll(),
            countryVMs = _countryService.GetAll(),
            UserSkillVMs = _userSkillService.GetUserSkillsByUserId(user.UserId)
        };
    }

    public List<UserVM> GetAll()
    {
        IEnumerable<User> users = _unitOfWork.User.GetAll();
        if(users == null)return null!;
        return users.Select(user => ConvertUserToVM(user)).ToList();
    }

    public void Add(UserVM userVM)
    {
        User obj = new User()
        {
            FirstName = userVM.FirstName,
            LastName = userVM.LastName,
            Email = userVM.Email,
            Password = userVM.Password,
            PhoneNumber = userVM.PhoneNumber,
            CreatedAt = userVM.CreatedAt,
            WhyIVolunteer = userVM.WhyIVolunteer,
            ProfileText = userVM.ProfileText,
            Avatar = userVM.Avatar
        };
        _unitOfWork.User.Add(obj);
    }

    public UserVM Login(LoginVM loginVM)
    {
        Expression<Func<User, bool>> filter = user => (user.Email == loginVM.Email && user.Password == loginVM.Password);
        User obj = _unitOfWork.User.GetFirstOrDefault(filter);
        return obj == null ? null! : ConvertUserToVM(obj);
    }

    public UserVM GetFirstOrDefaultByEmail(string email)
    {
        User obj = _unitOfWork.User.GetFirstOrDefault(user => user.Email.Equals(email));
        return obj == null ? null! : ConvertUserToVM(obj);
    }
    public ProfileVM GetUserProfileById(long userId)
    {
        User obj = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == userId);
        return obj == null ? null! : ConvertProfileToVM(obj);
    }
    public void UpdateUserProfile(ProfileVM profileVM)
    {
        User user = _unitOfWork.User.GetFirstOrDefault(user => user.UserId == profileVM.UserId);
        user.FirstName = profileVM.FirstName;
        user.LastName = profileVM.LastName;
        user.LinkedInUrl = profileVM.LinkedInUrl;
        user.Availability = profileVM.Availability;
        user.Avatar = profileVM.Avatar;
        user.UpdatedAt = DateTimeOffset.Now;
        user.CityId = profileVM.CityId;
        user.CountryId = profileVM.CountryId;
        user.Department = profileVM.Department;
        user.EmployeeId = profileVM.EmployeeId;
        user.Title = profileVM.Title;
        user.ProfileText = profileVM.ProfileText;
        user.WhyIVolunteer = profileVM.WhyIVolunteer;
        user.PhoneNumber = profileVM.PhoneNumber;
    }
    public void SendResetPasswordEmail(string email, string url)
    {
        string subject = "CI Platform - Reset-Password link";
        string link = $"<a href='{url}' style='text-decoration:none;display:block;width:max-content;border:1px solid black;border-radius:5rem;padding:0.75rem 1rem;margin:1rem auto;color:black;font-size:1rem;'>Reset Password</a>";
        string body = $"<p style='text-align:center;font-size:1.5rem'>Click on the link below to reset your password</p><hr/>{link}";
        _emailService.SendEmail(email, subject, body);
    }

    public bool IsPasswordValid(string email, string password)
    {
        User user = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);
        if (user == null) return false;
        if(user.Password != password) return false;
        return true;
    }
    public void UpdatePassword(string email, string password)
    {
        _unitOfWork.User.UpdatePassword(email, password);
    }

    public List<UserVM> GetAllUsersToRecommendMission()
    {
        List<User> users = _unitOfWork.User.GetAllToRecommendMission();
        if (users == null) return null!;
        return users.Select(user => ConvertUserToVM(user)).ToList();
    }
    public List<UserVM> GetAllUsersToRecommendStory()
    {
        List<User> users = _unitOfWork.User.GetAllToRecommendStory();
        if (users == null) return null!;
        return users.Select(user => ConvertUserToVM(user)).ToList();
    }
    internal static List<MissionInviteVM> GetMissionInviteFrom(User user)
    {
        return user?.MissionInviteFromUsers?.LongCount() > 0 ? 
            user.MissionInviteFromUsers.Select(mi => MissionInviteService.ConvertMissionInviteToVM(mi)).ToList() : new();
    }
    internal static List<MissionInviteVM> GetMissionInviteTo(User user)
    {
        return user?.MissionInviteToUsers?.LongCount() > 0 ?
            user.MissionInviteToUsers.Select(mi => MissionInviteService.ConvertMissionInviteToVM(mi)).ToList() : new();
    }
    internal static List<StoryInviteVM> GetStoryInviteFrom(User user)
    {
        return user?.StoryInviteFromUsers?.LongCount() > 0 ?
            user.StoryInviteFromUsers.Select(mi => StoryInviteService.ConvertStoryInviteToVM(mi)).ToList() : new();
    }
    internal static List<StoryInviteVM> GetStoryInviteTo(User user)
    {
        return user?.StoryInviteToUsers?.LongCount() > 0 ?
            user.StoryInviteToUsers.Select(mi => StoryInviteService.ConvertStoryInviteToVM(mi)).ToList() : new();
    }
}