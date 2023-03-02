using CI_Platform.Services.Service.Interface;
using CI_Platform.DataAccess.Repository.Interface;

namespace CI_Platform.Services.Service;

public class UnitOfService : IUnitOfService
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfService(IUnitOfWork unitOfWork, IEmailService emailService)
    {   
        _unitOfWork = unitOfWork;
        User = new UserService(_unitOfWork, emailService);
        ResetPassword = new ResetPasswordService(_unitOfWork);
    }

    public IUserService User{get; private set;}
    public IResetPasswordService ResetPassword{get; private set;}


    public void Save(){
        _unitOfWork.Save();
    }
}
