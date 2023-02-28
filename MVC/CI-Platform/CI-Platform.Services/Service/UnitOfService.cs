using CI_Platform.Services.Service.Interface;
using CI_Platform.DataAccess.Repository.Interface;

namespace CI_Platform.Services.Service;

public class UnitOfService : IUnitOfService
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfService(IUnitOfWork unitOfWork)
    {   
        _unitOfWork = unitOfWork;
        User = new UserService(_unitOfWork);
    }

    public IUserService User{get; private set;}
}
