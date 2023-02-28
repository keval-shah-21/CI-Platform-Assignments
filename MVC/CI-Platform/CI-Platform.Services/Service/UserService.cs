using CI_Platform.Services.Service.Interface;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Entities.DataModels;
using CI_Platform.DataAccess.Repository.Interface;
namespace CI_Platform.Services.Service;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Add(UserVM user){
        User obj = new User(){
        };
    }
}
