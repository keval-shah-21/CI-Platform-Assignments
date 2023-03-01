using CI_Platform.Services.Service.Interface;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Entities.DataModels;
using CI_Platform.DataAccess.Repository.Interface;

using System.Linq.Expressions;

namespace CI_Platform.Services.Service;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Add(UserVM userVM){
        User obj = new User(){
            FirstName = userVM.FirstName,
            LastName = userVM.LastName,
            Email = userVM.Email,
            Password = userVM.Password,
            PhoneNumber = userVM.PhoneNumber
        };
        _unitOfWork.User.Add(obj);
    }

    public UserVM Login(LoginVM loginVM){
        Expression<Func<User, bool>> filter = user => (user.Email == loginVM.Email && user.Password == loginVM.Password);
        User obj = _unitOfWork.User.GetFirstOrDefault(filter);
        if(obj != null)
            return new UserVM(){
                UserId = obj.UserId,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Email = obj.Email,
                Password = obj.Password,
                PhoneNumber = obj.PhoneNumber,
            };
        return null!;
    }

    public UserVM GetFirstOrDefaultByEmail(string Email){
        User obj = _unitOfWork.User.GetFirstOrDefault(user => user.Email.Equals(Email));
        if(obj != null){
            return new UserVM(){
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
}