using CI_Platform.Services.Service.Interface;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Entities.DataModels;
using CI_Platform.DataAccess.Repository.Interface;
using System.Linq.Expressions;
using CI_Platform.Entities.Constants;
using Microsoft.AspNetCore.Http;

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
            WhyIVolunteer = user.WhyIVolunteer,
            ProfileText = user.ProfileText,
            PhoneNumber = user.PhoneNumber,
            EmployeeId = user.EmployeeId,
            Department = user.Department,
            Status = user.Status,
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
            WhyIVolunteer = user.WhyIVolunteer,
            ProfileText = user.ProfileText,
            PhoneNumber = user.PhoneNumber,
            Department = user.Department,
            LinkedInUrl = user.LinkedInUrl,
            EmployeeId = user.EmployeeId,
            Title = user.Title,
            Availability = (Availability)(user.Availability ?? 0),
            CityId = user.CityId,
            CountryId = user.CountryId,
            skillVMs = _skillService.GetAll(),
            cityVMs = _cityService.GetAll(),
            countryVMs = _countryService.GetAll(),
            UserSkillVMs = _userSkillService.GetUserSkillsByUserId(user.UserId)
        };
    }
    public static UserAdminVM ConvertUserAdminToVM(User user)
    {
        return new UserAdminVM()
        {
            UserId = user.UserId,
            Avatar = user.Avatar,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CityId = user.CityId,
            CountryId = user.CountryId,
            ProfileText = user.ProfileText,
            PhoneNumber = user.PhoneNumber,
            EmployeeId = user.EmployeeId,
            Department = user.Department,
            Status = user.Status,
        };
    }

    public List<UserVM> GetAll()
    {
        IEnumerable<User> users = _unitOfWork.User.GetAll();
        if (users == null) return null!;
        return users.Select(user => ConvertUserToVM(user)).ToList();
    }

    public long Add(UserVM userVM)
    {
        User obj = new User()
        {
            FirstName = userVM.FirstName,
            LastName = userVM.LastName,
            Email = userVM.Email,
            Password = EncryptionService.EncryptAES(userVM.Password),
            PhoneNumber = userVM.PhoneNumber,
            CreatedAt = userVM.CreatedAt,
            WhyIVolunteer = userVM.WhyIVolunteer,
            ProfileText = userVM.ProfileText,
            Avatar = userVM.Avatar,
            Status = false
        };
        _unitOfWork.User.Add(obj);
        _unitOfWork.Save();
        return obj.UserId;
    }
    public void AddUserAdmin(UserAdminVM user)
    {
        User obj = new User()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = EncryptionService.EncryptAES(user.Password),
            PhoneNumber = user.PhoneNumber,
            CreatedAt = DateTimeOffset.Now,
            ProfileText = user.ProfileText,
            Avatar = user.Avatar,
            Status = false,
            CityId = user.CityId,
            CountryId = user.CountryId,
            EmployeeId = user.EmployeeId,
            Department = user.Department,
        };
        _unitOfWork.User.Add(obj);
    }
    public AdminVM AdminLogin(LoginVM loginVM)
    {
        Expression<Func<Admin, bool>> filter = user => (user.Email == loginVM.Email && user.Password == EncryptionService.EncryptAES(loginVM.Password));
        Admin obj = _unitOfWork.Admin.GetFirstOrDefault(filter);
        return obj == null ? null! : new AdminVM()
        {
            AdminId = obj.AdminId,
            FirstName = obj.FirstName,
            LastName = obj.LastName,
            Email = obj.Email,
            Avatar = obj.Avatar
        };
    }

    public UserVM Login(LoginVM loginVM)
    {
        Expression<Func<User, bool>> filter = user => (user.Email == loginVM.Email && user.Password == EncryptionService.EncryptAES(loginVM.Password));
        User obj = _unitOfWork.User.GetFirstOrDefault(filter);
        return obj == null ? null! : ConvertUserToVM(obj);
    }

    public UserVM GetFirstOrDefaultByEmail(string email)
    {
        User obj = _unitOfWork.User.GetFirstOrDefault(user => user.Email.Equals(email));
        return obj == null ? null! : ConvertUserToVM(obj);
    }
    public async Task<UserVM> GetFirstOrDefaultById(long id)
    {
        User obj = await _unitOfWork.User.GetFirstOrDefaultAsync(user => user.UserId == id);
        return ConvertUserToVM(obj);
    }
    public UserAdminVM GetFirstOrDefaultUserAdmin(long id)
    {
        User obj = _unitOfWork.User.GetFirstOrDefault(user => user.UserId == id);
        return obj == null ? null! : ConvertUserAdminToVM(obj);
    }

    public string SaveProfileImage(string wwwRootPath, IFormFile profileInput)
    {
        string fileName = Guid.NewGuid().ToString();
        var uploads = Path.Combine(wwwRootPath, @"images\user");
        string extension = Path.GetExtension(profileInput.FileName);
        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
        {
            profileInput.CopyTo(fileStreams);
        }
        return @"\images\user\" + fileName + extension;
    }
    public void RemoveProfileImage(string wwwRootPath, string preloadedImage)
    {
        string oldPath = Path.Combine(wwwRootPath, preloadedImage.TrimStart('\\'));
        bool isDefault = preloadedImage.Split("\\").Last().Split(".")[0].Equals("default-profile");
        if (System.IO.File.Exists(oldPath) && !isDefault)
        {
            System.IO.File.Delete(oldPath);
        }
    }
    public ProfileVM GetUserProfileById(long userId)
    {
        User obj = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == userId);
        return obj == null ? throw new Exception("User profile not found") : ConvertProfileToVM(obj);
    }
    public void UpdateUserProfile(ProfileVM profileVM)
    {
        User user = _unitOfWork.User.GetFirstOrDefault(user => user.UserId == profileVM.UserId);
        user.FirstName = profileVM.FirstName;
        user.LastName = profileVM.LastName;
        user.LinkedInUrl = profileVM.LinkedInUrl;
        user.Availability = (byte?)profileVM.Availability;
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
    public void UpdateUserAdmin(UserAdminVM userAdmin)
    {
        User user = _unitOfWork.User.GetFirstOrDefault(user => user.UserId == userAdmin.UserId);
        user.FirstName = userAdmin.FirstName;
        user.LastName = userAdmin.LastName;
        user.PhoneNumber = userAdmin.PhoneNumber;
        user.UpdatedAt = DateTimeOffset.Now;
        user.ProfileText = userAdmin.ProfileText;
        user.Avatar = userAdmin.Avatar;
        user.Status = (bool)userAdmin.Status;
        user.CityId = userAdmin.CityId;
        user.CountryId = userAdmin.CountryId;
        user.EmployeeId = userAdmin.EmployeeId;
        user.Department = userAdmin.Department;
    }
    public bool IsProfileFilled(long id)
    {
        return _unitOfWork.User.GetFirstOrDefault(user => user.UserId == id).Availability != null;
    }
    public void SendResetPasswordEmail(string email, string url)
    {
        string subject = "CI Platform - Reset-Password link";
        string link = $"<a href='{url}' style='text-decoration:none;display:block;width:max-content;border:1px solid black;border-radius:5rem;padding:0.75rem 1rem;margin:1rem auto;color:black;font-size:1rem;'>Reset Password</a>";
        string body = $"<p style='text-align:center;font-size:1.5rem'>Click on the link below to reset your password</p><hr/>{link}";
        _emailService.SendEmail(email, subject, body);
    }
    public void SaveVerifyAccountDetails(string email, string token)
    {
        _unitOfWork.VerifyEmail.Add(new VerifyEmail()
        {
            Email = email,
            Token = token
        });
    }

    public bool VerifyEmail(string email, string token)
    {
        return _unitOfWork.VerifyEmail.GetFirstOrDefault(ve => ve.Email == email && ve.Token == token) != null;
    }
    public void RemoveVerifyEmail(string email)
    {
        _unitOfWork.VerifyEmail.Remove(_unitOfWork.VerifyEmail.GetFirstOrDefault(ve => ve.Email == email));
    }
    public void ActivateUserByEmail(string email)
    {
        _unitOfWork.User.ActivateUserByEmail(email);
    }
    public void DeactivateUserByEmail(string email)
    {
        _unitOfWork.User.DeactivateUserByEmail(email);
    }
    public void SendVerifyAccountEmail(string email, string url)
    {
        string subject = "CI Platform - Verify Account";
        string link = $"<a href='{url}' style='text-decoration:none;display:block;width:max-content;border:1px solid black;border-radius:5rem;padding:0.75rem 1rem;margin:1rem auto;color:black;font-size:1rem;'>Verify Account</a>";
        string body = $"<p style='text-align:center;font-size:1.5rem'>Click on the link below to verify your account</p><hr/>{link}";
        _emailService.SendEmail(email, subject, body);
    }
    public void SendAccountCreatedMail(string email, string password, string url)
    {
        string subject = "CI Platform - Account Created";
        string link = $"<a href='{url}' style='text-decoration:none;display:block;width:max-content;border:1px solid black;border-radius:5rem;padding:0.75rem 1rem;margin:1rem auto;color:black;font-size:1rem;'>Verify Account</a>";
        string body = $"<div style='font-size:1rem'><p>Your email address: {email}</p><p>Your password: {password}</p><p style='text-align:center;font-size:1.5rem'>Click on the link below to verify your account</p></div><hr/>{link}";
        _emailService.SendEmail(email, subject, body);
    }
    public bool IsPasswordValid(string email, string password)
    {
        User user = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);
        if (user == null) return false;
        if (user.Password != EncryptionService.EncryptAES(password)) return false;
        return true;
    }
    public void UpdatePassword(string email, string password)
    {
        _unitOfWork.User.UpdatePassword(email, EncryptionService.EncryptAES(password));
    }
    public List<UserVM> GetAllUsersToRecommendMission()
    {
        List<User> users = _unitOfWork.User.GetAllToRecommendMission();
        if (users == null) return null!;
        return users.Select(user => ConvertUserToVM(user)).ToList();
    }
    public List<UserVM> SearchUser(string? query)
    {
        IEnumerable<User> users = _unitOfWork.User.GetAll();
        return string.IsNullOrEmpty(query) ? users.Select(u => ConvertUserToVM(u)).ToList()
            : users
                .Where(u => (u.FirstName.ToLower() + ' ' + u.LastName.ToLower()).Contains(query.ToLower()))
                .Select(u => ConvertUserToVM(u))
                .ToList();
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