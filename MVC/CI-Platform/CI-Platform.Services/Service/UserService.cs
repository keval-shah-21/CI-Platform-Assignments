using CI_Platform.Services.Service.Interface;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Entities.DataModels;
using CI_Platform.DataAccess.Repository.Interface;
using System.Linq.Expressions;

namespace CI_Platform.Services.Service;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public UserService(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public void Add(UserVM userVM)
    {
        User obj = new User()
        {
            FirstName = userVM.FirstName,
            LastName = userVM.LastName,
            Email = userVM.Email,
            Password = userVM.Password,
            PhoneNumber = userVM.PhoneNumber
        };
        _unitOfWork.User.Add(obj);
    }

    public UserVM Login(LoginVM loginVM)
    {
        Expression<Func<User, bool>> filter = user => (user.Email == loginVM.Email && user.Password == loginVM.Password);
        User obj = _unitOfWork.User.GetFirstOrDefault(filter);
        if (obj != null)
            return new UserVM()
            {
                UserId = obj.UserId,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Email = obj.Email,
                Password = obj.Password,
                PhoneNumber = obj.PhoneNumber,
            };
        return null!;
    }

    public UserVM GetFirstOrDefaultByEmail(string email)
    {
        User obj = _unitOfWork.User.GetFirstOrDefault(user => user.Email.Equals(email));
        if (obj != null)
        {
            return new UserVM()
            {
                UserId = obj.UserId,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Email = obj.Email,
                Password = obj.Password,
                PhoneNumber = obj.PhoneNumber,
            };
        }
        return null!;
    }

    public void SendResetPasswordEmail(string email, string url)
    {
        string subject = "CI Platform - Reset-Password link";
        string link = $"<a href='{url}' style='text-decoration:none;display:block;width:max-content;border:1px solid black;border-radius:5rem;padding:0.75rem 1rem;margin:1rem auto;color:black;font-size:1rem;'>Reset Password</a>";
        string body = $"<p style='text-align:center;font-size:1.5rem'>Click on the link below to reset your password</p><hr/>{link}";
        _emailService.SendEmail(email, subject, body);
    }

    public void UpdateByPassword(string email, string password)
    {
        User user = _unitOfWork.User.GetFirstOrDefault(user => user.Email == email);
        user.Password = password;
        _unitOfWork.User.Update(user);
    }
}